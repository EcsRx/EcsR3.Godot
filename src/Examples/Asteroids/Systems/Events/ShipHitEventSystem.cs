using EcsR3.Collections.Database;
using EcsR3.Collections.Entity;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Events;
using R3;
using SystemsR3.Scheduling;
using SystemsR3.Systems.Conventional;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Events;

public class ShipHitEventSystem : IReactToEventSystem<MeteorCollidedWithShipEvent>
{
    public IEntityCollection EntityCollection { get; }
    public IUpdateScheduler UpdateScheduler { get; }
    
    public ShipHitEventSystem(IEntityDatabase entityDatabase, IUpdateScheduler updateScheduler)
    {
        UpdateScheduler = updateScheduler;
        EntityCollection = entityDatabase.GetCollection();
    }

    public void Process(MeteorCollidedWithShipEvent eventData)
    {
        var playerComponent = eventData.Ship.GetComponent<PlayerComponent>();
        playerComponent.Score -= 500;

        if (playerComponent.Score < 0)
        { playerComponent.Score = 0; }
        
        UpdateScheduler.OnPostUpdate.Take(1).Subscribe(_ =>
        {
            if(EntityCollection.ContainsEntity(eventData.Meteor.Id))
            { EntityCollection.RemoveEntity(eventData.Meteor.Id); }
        });
    }
}