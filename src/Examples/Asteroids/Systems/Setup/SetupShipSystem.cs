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
using EcsR3.Systems.Reactive;
using Godot;
using Vector2 = System.Numerics.Vector2;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Setup;

public class SetupShipSystem : ISetupSystem
{
    public IGroup Group { get; } = new Group(typeof(PlayerComponent), typeof(ViewComponent), typeof(Transform2DComponent));
        
    public GameTextureResources GameTextureResources { get; }

    public SetupShipSystem(GameTextureResources gameTextureResources)
    {
        GameTextureResources = gameTextureResources;
    }

    public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
        
        var sprite = new Sprite2D();
        sprite.Texture = GameTextureResources.ShipTexture;
        viewComponent.View = sprite;
            
        var colliderComponent = entityComponentAccessor.GetComponent<ColliderComponent>(entity);
        colliderComponent.Width = sprite.Texture.GetWidth();
        colliderComponent.Height = sprite.Texture.GetHeight();
        
        var transformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        transformComponent.Transform.Position = new Vector2(0, 0);
        transformComponent.Transform.Scale = new Vector2(1, 1);
        transformComponent.Transform.Rotation = 0;
        
        viewComponent.SetTransform(transformComponent.Transform);
        this.AddToRootScene(sprite);
    }
}