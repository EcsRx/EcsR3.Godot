using EcsR3.Blueprints;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Types;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;

namespace EcsR3.Godot.Examples.Asteroids.Blueprints;

public class MeteorBlueprint : IBlueprint
{
    public MeteorType MeteorType { get; set; } = MeteorType.Big;
    
    public void Apply(IEntity entity)
    {
        var handlingComponent = new HandlingComponent()
        {
            MovementSpeed = 90f,
            RotationSpeed = 4.0f
        };

        var lifetimeComponent = new HasLifetimeComponent()
        {
            MaxAliveTime = 10.0f
        };

        var meteorComponent = new MeteorComponent()
        {
            Type = MeteorType
        };
        
        entity.AddComponents(handlingComponent, meteorComponent, lifetimeComponent, 
            new MoveableComponent(), new ColliderComponent(), new ViewComponent(), new Transform2DComponent());
    }
}