using EcsR3.Components;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class HandlingComponent : IComponent
{
    public float MovementSpeed { get; set; }
    public float RotationSpeed { get; set; }
}