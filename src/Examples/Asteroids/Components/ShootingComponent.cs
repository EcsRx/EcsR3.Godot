using EcsR3.Components;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class ShootingComponent : IComponent
{
    public float FireTimeLeft { get; set; }
    public bool IsFiring { get; set; }
}