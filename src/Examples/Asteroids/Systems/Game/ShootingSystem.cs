using System.Numerics;
using EcsR3.Collections.Database;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Blueprints;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems;
using SystemsR3.Plugins.Transforms.Extensions;
using SystemsR3.Scheduling;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Game;

public class ShootingSystem : IBasicEntitySystem
{
    public float FireRateDelay = 0.5f;
    
    public IGroup Group { get; } = new Group(typeof(ShootingComponent), typeof(Transform2DComponent));
    
    public IEntityCollection EntityCollection { get; }

    public ShootingSystem(IEntityDatabase entityDatabase)
    {
        EntityCollection = entityDatabase.GetCollection();
    }

    public void Process(IEntity entity, ElapsedTime elapsedTime)
    {
        var shootingComponent = entity.GetComponent<ShootingComponent>();
        if (shootingComponent.FireTimeLeft > 0)
        { shootingComponent.FireTimeLeft -= (float)elapsedTime.DeltaTime.TotalSeconds; }

        if (shootingComponent.IsFiring && shootingComponent.FireTimeLeft <= 0)
        {
            shootingComponent.FireTimeLeft += FireRateDelay;
            CreateShotFor(entity);
        }
    }

    public void CreateShotFor(IEntity shooterEntity)
    {
        var shooterTransformComponent = shooterEntity.GetComponent<Transform2DComponent>();
        var shooterTransform = shooterTransformComponent.Transform;

        var projectileEntity = EntityCollection.CreateEntity<ProjectileBlueprint>();
        var projectileTransformComponent = projectileEntity.GetComponent<Transform2DComponent>();
        var projectileTransform = projectileTransformComponent.Transform;
        projectileTransform.Position = shooterTransform.Position;
        projectileTransform.Rotation = shooterTransform.Rotation;
        projectileTransform.Position += projectileTransform.Forward() * 64.0f;

        var projectileComponent = projectileEntity.GetComponent<ProjectileComponent>();
        projectileComponent.PlayerEntityId = shooterEntity.Id;
        
        var moveableComponent = projectileEntity.GetComponent<MoveableComponent>();
        moveableComponent.MovementChange = new Vector2(0, 1); // No Strafe, Just Forwards
    }
}