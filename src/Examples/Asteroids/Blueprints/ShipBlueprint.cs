using EcsR3.Blueprints;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;

namespace EcsR3.Godot.Examples.Asteroids.Blueprints;

public class ShipBlueprint : IBlueprint
{
    public void Apply(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var handlingComponent = new HandlingComponent()
        {
            MovementSpeed = 100f,
            RotationSpeed = 5.0f
        };
            
        entityComponentAccessor.AddComponents(entity, new PlayerComponent(), handlingComponent, new ColliderComponent(), 
            new ShootingComponent(), new MoveableComponent(), new ViewComponent(), new Transform2DComponent());
    }
}