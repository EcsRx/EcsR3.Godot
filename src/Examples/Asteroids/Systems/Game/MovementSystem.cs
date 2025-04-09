using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems;
using SystemsR3.Attributes;
using SystemsR3.Plugins.Transforms.Extensions;
using SystemsR3.Scheduling;
using SystemsR3.Types;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Game;

[Priority(PriorityTypes.Higher)]
public class MovementSystem : IBasicEntitySystem
{
    public IGroup Group { get; } = new Group(typeof(HandlingComponent), typeof(MoveableComponent), typeof(Transform2DComponent));

    public void Process(IEntity entity, ElapsedTime elapsedTime)
    {
        var handlingComponent = entity.GetComponent<HandlingComponent>();
        var moveableComponent = entity.GetComponent<MoveableComponent>();
        var transformComponent = entity.GetComponent<Transform2DComponent>();

        var movementSpeed = handlingComponent.MovementSpeed * (float)elapsedTime.DeltaTime.TotalSeconds;
        var rotationSpeed = handlingComponent.RotationSpeed * (float)elapsedTime.DeltaTime.TotalSeconds;
            
        var transform = transformComponent.Transform;
        transform.Position += transform.Forward() * moveableComponent.MovementChange.Y * movementSpeed;
        transform.Position += transform.Right() * moveableComponent.MovementChange.X * movementSpeed;
        transform.Rotation += moveableComponent.DirectionChange * rotationSpeed;
    }
}