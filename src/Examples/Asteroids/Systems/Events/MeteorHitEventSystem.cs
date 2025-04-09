using System;
using System.Numerics;
using EcsR3.Collections.Database;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Blueprints;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Events;
using EcsR3.Godot.Examples.Asteroids.Types;
using EcsR3.Plugins.Transforms.Components;
using R3;
using SystemsR3.Plugins.Transforms.Models;
using SystemsR3.Scheduling;
using SystemsR3.Systems.Conventional;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Events;

public class MeteorHitEventSystem : IReactToEventSystem<MeteorCollidedWithProjectileEvent>
{
    public IEntityCollection EntityCollection { get; }
    public IUpdateScheduler UpdateScheduler { get; }
    public Random Random { get; } = new Random();

    public MeteorHitEventSystem(IEntityDatabase entityDatabase, IUpdateScheduler updateScheduler)
    {
        UpdateScheduler = updateScheduler;
        EntityCollection = entityDatabase.GetCollection();
    }

    public void Process(MeteorCollidedWithProjectileEvent eventData)
    {
        var meteorComponent = eventData.Meteor.GetComponent<MeteorComponent>();

        var projectileComponent = eventData.Projectile.GetComponent<ProjectileComponent>();
        var owningShip = EntityCollection.GetEntity(projectileComponent.PlayerEntityId);
        
        var playerComponent = owningShip.GetComponent<PlayerComponent>();
        playerComponent.Score += CalculateScoreFor(meteorComponent);

        SpawnNewMeteorsIfNeeded(eventData.Meteor);
       
        UpdateScheduler.OnPostUpdate.Take(1).Subscribe(_ =>
        {
            // The Lifecycle system may have removed these during the update cycle
            // so we need to confirm they still exist
            if(EntityCollection.ContainsEntity(eventData.Meteor.Id))
            { EntityCollection.RemoveEntity(eventData.Meteor.Id); }
            
            if(EntityCollection.ContainsEntity(eventData.Projectile.Id))
            { EntityCollection.RemoveEntity(eventData.Projectile.Id); }
        });
    }

    public void SpawnNewMeteorsIfNeeded(IEntity meteorEntity)
    {
        var meteorComponent = meteorEntity.GetComponent<MeteorComponent>();
        if (meteorComponent.Type == MeteorType.Small) { return; }

        var transformComponent = meteorEntity.GetComponent<Transform2DComponent>();
        var parentTransform = transformComponent.Transform;

        var newMeteor1 = EntityCollection.CreateEntity(new MeteorBlueprint { MeteorType = meteorComponent.Type + 1 });
        SetupChildTransforms(newMeteor1, parentTransform);
        
        var newMeteor2 = EntityCollection.CreateEntity(new MeteorBlueprint { MeteorType = meteorComponent.Type + 1 });
        SetupChildTransforms(newMeteor2, parentTransform);
    }

    public void SetupChildTransforms(IEntity childMeteor, Transform2D parentTransform)
    {
        var transformComponent = childMeteor.GetComponent<Transform2DComponent>();
        var transform = transformComponent.Transform;

        var positionOffset = new Vector2(Random.Next(-64, 64));
        transform.Position = parentTransform.Position + positionOffset;
        var rotationOffset = Random.NextSingle();
        transform.Rotation = parentTransform.Rotation + rotationOffset;
    }

    private int CalculateScoreFor(MeteorComponent meteorComponent)
    {
        switch (meteorComponent.Type)
        {
            case MeteorType.Medium: return 200;
            case MeteorType.Small: return 500;
            case MeteorType.Big:
            default: return 100;
        }
    }
}