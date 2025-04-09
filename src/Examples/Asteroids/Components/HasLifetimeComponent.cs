using EcsR3.Components;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class HasLifetimeComponent : IComponent
{
    public float MaxAliveTime { get; set; }
    public float AliveTime { get; set; }
}