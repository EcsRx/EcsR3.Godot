using System;
using EcsR3.Collections.Database;
using EcsR3.Collections.Entity;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Blueprints;
using R3;
using SystemsR3.Systems.Conventional;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Game;

public class MeteorSpawningSystem : IReactiveSystem<Unit>
{
    public IEntityCollection EntityCollection { get; }
    
    private double _elapsedTime;

    public MeteorSpawningSystem(IEntityDatabase entityDatabase)
    {
        EntityCollection = entityDatabase.GetCollection();
    }

    public Observable<Unit> ReactTo() => Observable.Interval(TimeSpan.FromSeconds(0.5f));

    public void Execute(Unit _)
    {
        EntityCollection.CreateEntity<MeteorBlueprint>();
    }
}