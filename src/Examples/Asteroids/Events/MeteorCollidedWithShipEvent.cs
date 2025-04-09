using EcsR3.Entities;

namespace EcsR3.Godot.Examples.Asteroids.Events;

public class MeteorCollidedWithShipEvent
{
    public IEntity Meteor { get; set; }
    public IEntity Ship { get; set; }
}