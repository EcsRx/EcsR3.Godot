using EcsR3.Collections.Database;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Groups;
using EcsR3.Systems;
using R3;
using SystemsR3.Scheduling;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Game;

public class LifetimeClearingSystem : IBasicEntitySystem
{
    public IGroup Group { get; } = new Group(typeof(HasLifetimeComponent));
    public IEntityCollection EntityCollection { get; }
    public IUpdateScheduler UpdateScheduler { get; }

    public LifetimeClearingSystem(IEntityDatabase entityDatabase, IUpdateScheduler updateScheduler)
    {
        UpdateScheduler = updateScheduler;
        EntityCollection = entityDatabase.GetCollection();
    }

    public void Process(IEntity entity, ElapsedTime elapsedTime)
    {
        var lifetimeComponent = entity.GetComponent<HasLifetimeComponent>();
        lifetimeComponent.AliveTime += (float)elapsedTime.DeltaTime.TotalSeconds;

        if (lifetimeComponent.AliveTime >= lifetimeComponent.MaxAliveTime)
        {
            UpdateScheduler.OnPostUpdate.Take(1).Subscribe(_ => {
                EntityCollection.RemoveEntity(entity.Id);
            });
        }
    }
}