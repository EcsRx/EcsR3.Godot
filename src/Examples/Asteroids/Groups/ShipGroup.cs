using System;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;

namespace EcsR3.Godot.Examples.Asteroids.Groups;

public class ShipGroup : IGroup
{
    public Type[] RequiredComponents { get; } = [typeof(PlayerComponent), typeof(ShootingComponent), typeof(ColliderComponent), typeof(HandlingComponent), typeof(MoveableComponent), typeof(ViewComponent), typeof(Transform2DComponent)];
    public Type[] ExcludedComponents { get; } = Type.EmptyTypes;
}