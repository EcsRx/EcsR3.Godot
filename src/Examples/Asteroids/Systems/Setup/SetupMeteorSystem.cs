using System;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Extensions;
using EcsR3.Godot.Examples.Asteroids.Services;
using EcsR3.Godot.Examples.Asteroids.Types;
using EcsR3.Godot.Plugins.EcsR3.Godot.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems.Reactive;
using Godot;
using SystemsR3.Plugins.Transforms.Extensions;
using Vector2 = System.Numerics.Vector2;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Setup;

public class SetupMeteorSystem : ISetupSystem
{
    public IGroup Group { get; } = new Group(typeof(MeteorComponent), typeof(ViewComponent), typeof(Transform2DComponent), typeof(ColliderComponent), typeof(MoveableComponent));
        
    public Random Random { get; } = new Random();
    
    public GameTextureResources GameTextureResources { get; }

    public SetupMeteorSystem(GameTextureResources gameTextureResources)
    {
        GameTextureResources = gameTextureResources;
    }

    public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
        
        var sprite = new Sprite2D();
        sprite.Texture = GameTextureResources.MeteorTexture;
        viewComponent.View = sprite;
        
        var colliderComponent = entityComponentAccessor.GetComponent<ColliderComponent>(entity);
        colliderComponent.Width = sprite.Texture.GetWidth();
        colliderComponent.Height = sprite.Texture.GetHeight();
            
        var moveableComponent = entityComponentAccessor.GetComponent<MoveableComponent>(entity);
        moveableComponent.MovementChange = new Vector2(0, 1);

        SetupTransform(entityComponentAccessor, entity);
        
        var transformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        viewComponent.SetTransform(transformComponent.Transform);
        
        this.AddToRootScene(sprite);
    }

    public void SetupTransform(IEntityComponentAccessor entityComponentAccessor,  Entity entity)
    {
        var meteorComponent = entityComponentAccessor.GetComponent<MeteorComponent>(entity);
        var transformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        var viewTransform = transformComponent.Transform;
        viewTransform.Scale = Vector2.One / (int)meteorComponent.Type;
        if (meteorComponent.Type != MeteorType.Big) { return; }

        var spawnPosition = GetRandomSpawnPosition();
        var targetPosition = GetTargetPosition();
        viewTransform.Position = spawnPosition;
        viewTransform.Rotation = -viewTransform.GetLookAt(targetPosition);
    }
        
    public Vector2 GetRandomSpawnPosition()
    {
        var bufferAmount = 64;
        var nativeSize = DisplayServer.WindowGetSize();
        var spawnAreaWidth = nativeSize.X/2 + bufferAmount;
        var spawnAreaHeight = nativeSize.Y/2 + bufferAmount;
            
        var isVerticalSpawn = Random.NextBool();

        if (isVerticalSpawn)
        {
            var randomY = Random.Next(-spawnAreaHeight, spawnAreaHeight);
            var leftOrRight = Random.NextBool() ? -spawnAreaWidth : spawnAreaWidth;
            return new Vector2(leftOrRight, randomY);
        }
            
        var randomX = Random.Next(-spawnAreaWidth, spawnAreaWidth);
        var topOrBottom = Random.NextBool() ? -spawnAreaHeight : spawnAreaHeight;
        return new Vector2(randomX, topOrBottom);
    }

    public Vector2 GetTargetPosition()
    {
        var range = 64;
        var varianceX = Random.Next(-range, range);
        var varianceY = Random.Next(-range, range);
        return new Vector2(varianceX, varianceY);
    }
}