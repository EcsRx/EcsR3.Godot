using System.Drawing;
using System.Numerics;
using EcsR3.Godot.Examples.Asteroids.Components;
using SystemsR3.Plugins.Transforms.Extensions;
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
    
    /// <summary>
    /// This is an GD fudged version of Right
    /// </summary>
    /// <remarks>https://forum.godotengine.org/t/in-which-direction-does-the-node2d-rotation-point/224 apparently GD faces along X at 0 not Y</remarks>
    public static Vector2 GDRight(this Transform2D transform)
    {
        return (transform.Rotation).RadiansToVector2();
    }
    /// <summary>
    /// This is an GD fudged version of Forward
    /// </summary>
    /// <remarks>https://forum.godotengine.org/t/in-which-direction-does-the-node2d-rotation-point/224 apparently GD faces along X at 0 not Y</remarks>
    public static Vector2 GDForward(this Transform2D transform)
    {
        return (transform.Rotation + MathConstants.ToRadians(-90)).RadiansToVector2();
    }
    
    /// <summary>
    /// This is an GD version of LookAt
    /// </summary>
    /// <remarks>https://forum.godotengine.org/t/in-which-direction-does-the-node2d-rotation-point/224 apparently GD faces along X at 0 not Y</remarks>
    public static float GDLookAt(this Transform2D transform, Vector2 target)
    {
        return transform.GetLookAt(target) + MathConstants.ToRadians(90);
    }
}