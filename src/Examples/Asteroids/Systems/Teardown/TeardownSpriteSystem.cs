using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Services;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using Godot;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Teardown;

public class TeardownSpriteSystem : ITeardownSystem
{
    public IGroup Group { get; } = new Group(typeof(ViewComponent), typeof(Transform2DComponent));
        
    public GameTextureResources GameTextureResources { get; }

    public TeardownSpriteSystem(GameTextureResources gameTextureResources)
    {
        GameTextureResources = gameTextureResources;
    }

    public void Teardown(IEntity entity)
    {
        var viewComponent = entity.GetComponent<ViewComponent>();
        var sprite = viewComponent.View as Sprite2D;
        sprite.QueueFree();
    }
}