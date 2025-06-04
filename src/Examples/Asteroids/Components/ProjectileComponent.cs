using EcsR3.Components;
using EcsR3.Entities;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class ProjectileComponent : IComponent
{
    public Entity PlayerEntity { get; set; }
}