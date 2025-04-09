using EcsR3.Components;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class ColliderComponent : IComponent
{
    public float Width { get; set; }
    public float Height { get; set; }
}