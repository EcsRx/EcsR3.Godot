using EcsR3.Entities;

namespace EcsR3.Godot.Examples.Asteroids.Events;

public class MeteorCollidedWithProjectileEvent
{
    public IEntity Meteor { get; set; }
    public IEntity Projectile { get; set; }
}