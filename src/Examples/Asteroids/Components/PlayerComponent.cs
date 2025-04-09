using EcsR3.Components;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class PlayerComponent : IComponent
{
    public int PlayerIndex { get; set; }
    public int Score { get; set; }
}