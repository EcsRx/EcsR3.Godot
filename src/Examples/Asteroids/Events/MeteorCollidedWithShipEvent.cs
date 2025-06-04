using EcsR3.Entities;

namespace EcsR3.Godot.Examples.Asteroids.Events;

public readonly struct MeteorCollidedWithShipEvent
{
    public readonly Entity MeteorEntity;
    public readonly Entity ShipEntity;

    public MeteorCollidedWithShipEvent(Entity meteorEntity, Entity shipEntity)
    {
        MeteorEntity = meteorEntity;
        ShipEntity = shipEntity;
    }
}