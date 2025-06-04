using EcsR3.Entities;

namespace EcsR3.Godot.Examples.Asteroids.Events;

public readonly struct MeteorCollidedWithProjectileEvent
{
    public readonly Entity MeteorEntity;
    public readonly Entity ProjectileEntity;

    public MeteorCollidedWithProjectileEvent(Entity meteorEntity, Entity projectileEntity)
    {
        MeteorEntity = meteorEntity;
        ProjectileEntity = projectileEntity;
    }
}