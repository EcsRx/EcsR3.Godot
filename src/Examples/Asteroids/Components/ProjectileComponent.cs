using EcsR3.Components;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class ProjectileComponent : IComponent
{
    public int PlayerEntityId { get; set; }
}