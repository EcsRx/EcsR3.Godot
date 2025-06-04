using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Extensions;
using EcsR3.Godot.Examples.Asteroids.Services;
using EcsR3.Godot.Plugins.EcsR3.Godot.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using EcsR3.Systems.Reactive;
using Godot;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Setup;

public class SetupProjectileSystem : ISetupSystem
{
    public IGroup Group { get; } = new Group(typeof(ProjectileComponent), typeof(ViewComponent), typeof(Transform2DComponent), typeof(ColliderComponent), typeof(MoveableComponent));
        
    public GameTextureResources GameTextureResources { get; }

    public SetupProjectileSystem(GameTextureResources gameTextureResources)
    {
        GameTextureResources = gameTextureResources;
    }

    public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
        
        var sprite = new Sprite2D();
        sprite.Texture = GameTextureResources.LaserTexture;
        viewComponent.View = sprite;

        var colliderComponent = entityComponentAccessor.GetComponent<ColliderComponent>(entity);
        colliderComponent.Width = sprite.Texture.GetWidth();
        colliderComponent.Height = sprite.Texture.GetHeight();
        
        var transformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        viewComponent.SetTransform(transformComponent.Transform);
        this.AddToRootScene(sprite);
    }
}