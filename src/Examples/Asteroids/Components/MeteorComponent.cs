using EcsR3.Components;
using EcsR3.Godot.Examples.Asteroids.Types;

namespace EcsR3.Godot.Examples.Asteroids.Components;

public class MeteorComponent : IComponent
{
    public MeteorType Type { get; set; } = MeteorType.Big;
}