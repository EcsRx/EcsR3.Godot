using EcsR3.Computeds.Entities;
using EcsR3.Computeds.Entities.Registries;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Computed;
using EcsR3.Godot.Examples.Asteroids.Events;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems.Augments;
using EcsR3.Systems.Reactive;
using R3;
using SystemsR3.Events;
using SystemsR3.Scheduling;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Game;

public class MeteorCollisionDetectionSystem : IReactToGroupSystem, ISystemPreProcessor
{
    public IGroup Group { get; } = new Group(typeof(MeteorComponent), typeof(ColliderComponent), typeof(Transform2DComponent));
    
    public IUpdateScheduler UpdateScheduler { get; }
    public IEventSystem EventSystem { get; }
    public IComputedEntityGroup ShipComputedGroup { get; }
    public IComputedEntityGroup ProjectileComputedGroup { get; }
    public ComputedRuntimeColliders ComputedRuntimeColliders { get; }

    public MeteorCollisionDetectionSystem(IComputedEntityGroupRegistry computedEntityGroupRegistry, IEventSystem eventSystem, ComputedRuntimeColliders computedRuntimeColliders, IUpdateScheduler updateScheduler)
    {
        EventSystem = eventSystem;
        ComputedRuntimeColliders = computedRuntimeColliders;
        UpdateScheduler = updateScheduler;
        ShipComputedGroup = computedEntityGroupRegistry.GetComputedGroup(new Group(typeof(PlayerComponent), typeof(ColliderComponent), typeof(Transform2DComponent)));
        ProjectileComputedGroup = computedEntityGroupRegistry.GetComputedGroup(new Group(typeof(ProjectileComponent), typeof(ColliderComponent), typeof(Transform2DComponent)));
    }
    
    public Observable<IComputedEntityGroup> ReactToGroup(IComputedEntityGroup computedEntityGroup)
    { return UpdateScheduler.OnUpdate.Select(x => computedEntityGroup); }

    public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var colliders = ComputedRuntimeColliders.ComputedData;
        var meteorCollisionArea = colliders[entity.Id];

        foreach (var shipEntity in ShipComputedGroup)
        {
            var shipCollider = colliders[shipEntity.Id];
            if (meteorCollisionArea.IntersectsWith(shipCollider))
            { EventSystem.Publish(new MeteorCollidedWithShipEvent(entity, shipEntity)); }
        }
        
        foreach (var projectileEntity in ProjectileComputedGroup)
        {
            var shipCollider = colliders[projectileEntity.Id];
            if (meteorCollisionArea.IntersectsWith(shipCollider))
            { EventSystem.Publish(new MeteorCollidedWithProjectileEvent(entity, projectileEntity)); }
        }
    }

    public void BeforeProcessing()
    { ComputedRuntimeColliders.RefreshData(); }
}