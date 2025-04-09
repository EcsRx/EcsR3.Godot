using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Groups;
using EcsR3.Systems;
using Godot;
using SystemsR3.Attributes;
using SystemsR3.Scheduling;
using SystemsR3.Types;
using Vector2 = System.Numerics.Vector2;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Infrastructure;

[Priority(PriorityTypes.SuperHigh)]
public class HandleInputSystem : IBasicEntitySystem
{
    public IGroup Group { get; } = new Group(typeof(MoveableComponent), typeof(ShootingComponent), typeof(PlayerComponent));
        
    public void Process(IEntity entity, ElapsedTime elapsedTime)
    {
        var forwardChange = Input.GetAxis("backwards", "forwards");
        var strafeChange = Input.GetAxis("strafe_left", "strafe_right");
        var rotationChange = Input.GetAxis("rotate_left", "rotate_right");
        var isFiring = Input.IsActionPressed("fire");
        
        var moveableComponent = entity.GetComponent<MoveableComponent>();
        moveableComponent.MovementChange = new Vector2(strafeChange, forwardChange);
        moveableComponent.DirectionChange = rotationChange;

        var shootingComponent = entity.GetComponent<ShootingComponent>();
        shootingComponent.IsFiring = isFiring;
    }
}