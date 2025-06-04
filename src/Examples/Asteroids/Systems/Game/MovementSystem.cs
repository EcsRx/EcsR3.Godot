using EcsR3.Components.Database;
using EcsR3.Computeds.Components.Registries;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Extensions;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems.Augments;
using EcsR3.Systems.Batching.Convention;
using R3;
using SystemsR3.Attributes;
using SystemsR3.Plugins.Transforms.Extensions;
using SystemsR3.Scheduling;
using SystemsR3.Threading;
using SystemsR3.Types;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Game;

[Priority(PriorityTypes.Higher)]
public class MovementSystem : BatchedSystem<HandlingComponent, MoveableComponent, Transform2DComponent>, ISystemPreProcessor
{
    public ITimeTracker TimeTracker { get; }
    public IUpdateScheduler Scheduler { get; }
    
    private ElapsedTime _elapsedTime;
    
    public MovementSystem(IComponentDatabase componentDatabase, IEntityComponentAccessor entityComponentAccessor, IComputedComponentGroupRegistry computedComponentGroupRegistry, IThreadHandler threadHandler, ITimeTracker timeTracker, IUpdateScheduler scheduler) : base(componentDatabase, entityComponentAccessor, computedComponentGroupRegistry, threadHandler)
    {
        TimeTracker = timeTracker;
        Scheduler = scheduler;
    }

    protected override Observable<Unit> ReactWhen()
    { return Scheduler.OnUpdate.Select(x => Unit.Default); }

    protected override void Process(Entity entity, HandlingComponent handlingComponent, MoveableComponent moveableComponent,
        Transform2DComponent transformComponent)
    {
        var movementSpeed = handlingComponent.MovementSpeed * (float)_elapsedTime.DeltaTime.TotalSeconds;
        var rotationSpeed = handlingComponent.RotationSpeed * (float)_elapsedTime.DeltaTime.TotalSeconds;
            
        var transform = transformComponent.Transform;
        transform.Position += transform.GDForward() * moveableComponent.MovementChange.Y * movementSpeed;
        transform.Position += transform.GDRight() * moveableComponent.MovementChange.X * movementSpeed;
        transform.Rotation += moveableComponent.DirectionChange * rotationSpeed;
    }

    public void BeforeProcessing()
    { _elapsedTime = TimeTracker.ElapsedTime; }
}