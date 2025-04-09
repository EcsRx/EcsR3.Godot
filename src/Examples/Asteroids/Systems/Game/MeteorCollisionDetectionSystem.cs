using System.Linq;
using EcsR3.Collections;
using EcsR3.Entities;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Computed;
using EcsR3.Godot.Examples.Asteroids.Events;
using EcsR3.Groups;
using EcsR3.Groups.Observable;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems;
using R3;
using SystemsR3.Events;
using SystemsR3.Extensions;
using SystemsR3.Scheduling;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Game;

public class MeteorCollisionDetectionSystem : IReactToGroupExSystem
{
    public IGroup Group { get; } = new Group(typeof(MeteorComponent), typeof(ColliderComponent), typeof(Transform2DComponent));
    
    public IUpdateScheduler UpdateScheduler { get; }
    public IEventSystem EventSystem { get; }
    public IObservableGroup ShipObservableGroup { get; }
    public IObservableGroup ProjectileObservableGroup { get; }
    public ComputedRuntimeColliders RuntimeColliders { get; }

    public MeteorCollisionDetectionSystem(IObservableGroupManager observableGroupManager, IEventSystem eventSystem, ComputedRuntimeColliders runtimeColliders, IUpdateScheduler updateScheduler)
    {
        EventSystem = eventSystem;
        RuntimeColliders = runtimeColliders;
        UpdateScheduler = updateScheduler;
        ShipObservableGroup = observableGroupManager.GetObservableGroup(new Group(typeof(PlayerComponent), typeof(ColliderComponent), typeof(Transform2DComponent)));
        ProjectileObservableGroup = observableGroupManager.GetObservableGroup(new Group(typeof(ProjectileComponent), typeof(ColliderComponent), typeof(Transform2DComponent)));
    }
    
    public Observable<IObservableGroup> ReactToGroup(IObservableGroup observableGroup)
    { return UpdateScheduler.OnUpdate.Select(x => observableGroup); }

    public void Process(IEntity entity)
    {
        var meteorCollisionArea = RuntimeColliders[entity.Id];
        ShipObservableGroup
            .Where(x => meteorCollisionArea.IntersectsWith(RuntimeColliders[x.Id]))
            .ForEachRun(x => EventSystem.Publish(new MeteorCollidedWithShipEvent(){ Meteor = entity, Ship = x }));

        ProjectileObservableGroup
            .Where(x => meteorCollisionArea.IntersectsWith(RuntimeColliders[x.Id]))
            .ForEachRun(x => EventSystem.Publish(new MeteorCollidedWithProjectileEvent(){ Meteor = entity, Projectile = x }));
    }

    public void BeforeProcessing()
    { RuntimeColliders.RefreshData(); }

    public void AfterProcessing()
    {}
}