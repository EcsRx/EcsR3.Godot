using System.Drawing;
using EcsR3.Godot.Examples.Asteroids.Components;
using SystemsR3.Plugins.Transforms.Models;

namespace EcsR3.Godot.Examples.Asteroids.Extensions;

public static class Transform2DExtensions
{
    public static Rectangle GetCollisionArea(this Transform2D transform, ColliderComponent collider)
    {
        return new Rectangle(
        (int)transform.Position.X, 
        (int)transform.Position.Y,
        (int)collider.Width, (int)collider.Height); 
    }
}