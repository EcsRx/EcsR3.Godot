using EcsR3.Blueprints;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;

namespace EcsR3.Godot.Examples.Asteroids.Blueprints;

public class ProjectileBlueprint : IBlueprint
{
    public void Apply(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var handlingComponent = new HandlingComponent()
        {
            MovementSpeed = 500f,
            RotationSpeed = 4.0f
        };
        
        var lifetimeComponent = new HasLifetimeComponent()
        {
            MaxAliveTime = 5.0f
        };
        
        entityComponentAccessor.AddComponents(entity, handlingComponent, new ProjectileComponent(), lifetimeComponent, 
            new MoveableComponent(), new ColliderComponent(), new ViewComponent(), new Transform2DComponent());
    }
}