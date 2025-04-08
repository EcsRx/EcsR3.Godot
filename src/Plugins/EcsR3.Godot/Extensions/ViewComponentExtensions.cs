using EcsR3.Plugins.Views.Components;
using Godot;
using SystemsR3.Plugins.Transforms.Models;
using Transform2D = SystemsR3.Plugins.Transforms.Models.Transform2D;

namespace EcsR3.Godot.Plugins.EcsR3.Godot.Extensions;

public static class ViewComponentExtensions
{
    public static void SetTransform(this ViewComponent viewComponent, Transform transform)
    {
        if (viewComponent.View is not Node3D node3d)
        {
            GD.PushWarning("Cant convert View to Node3d");
            return;
        }

        node3d.Position = transform.Position.ToGodot();
        node3d.Quaternion = transform.Rotation.ToGodot();
        node3d.Scale = transform.Scale.ToGodot();
    }
    
    public static void SetTransform(this ViewComponent viewComponent, Transform2D transform)
    {
        if (viewComponent.View is not Node2D node2d)
        {
            GD.PushWarning("Cant convert View to Node2d");
            return;
        }

        node2d.Position = transform.Position.ToGodot();
        node2d.Rotation = transform.Rotation;
        node2d.Scale = transform.Scale.ToGodot();
    }
}