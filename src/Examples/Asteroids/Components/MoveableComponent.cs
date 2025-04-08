using EcsR3.Components;

namespace EcsR3.Godot.Examples.Asteroids.Components
{
    public class MoveableComponent : IComponent
    {
        public float MovementSpeed { get; set; }
    }
}