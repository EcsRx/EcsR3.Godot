using System.Drawing;
using EcsR3.Computeds.Collections;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Extensions;
using EcsR3.Groups.Observable;
using EcsR3.Plugins.Transforms.Components;
using R3;

namespace EcsR3.Godot.Examples.Asteroids.Computed;

public class ComputedRuntimeColliders : ComputedCollectionFromGroup<Rectangle>
{
    public ComputedRuntimeColliders(IObservableGroup internalObservableGroup) : base(internalObservableGroup)
    {}

    public override Observable<bool> RefreshWhen()
    { return Observable.Never<bool>(); }

    public override bool ShouldTransform(IEntity entity) => true;
    
    public override Rectangle Transform(IEntity entity) => GenerateCollisionArea(entity);
    
    public Rectangle GenerateCollisionArea(IEntity entity)
    {
        var colliderComponent = entity.GetComponent<ColliderComponent>();
        var transformComponent = entity.GetComponent<Transform2DComponent>();
        return transformComponent.Transform.GetCollisionArea(colliderComponent);
    }
}