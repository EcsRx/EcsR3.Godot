using System;
using Godot;
using SystemsR3.Systems;

namespace EcsR3.Godot.Examples.Asteroids.Extensions;

public static class ISystemExtensions
{
    public static SceneTree GetRootScene(this ISystem system)
    { return (SceneTree)Engine.GetMainLoop(); }

    public static void AddToRootScene(this ISystem system, Node node)
    {
        var sceneTree = (SceneTree)Engine.GetMainLoop();
        Callable.From(() => sceneTree.Root.AddChild(node)).CallDeferred();
    }

    public static void RunOnGodotThread(this ISystem system, Action action)
    { Callable.From(action).CallDeferred(); }
}