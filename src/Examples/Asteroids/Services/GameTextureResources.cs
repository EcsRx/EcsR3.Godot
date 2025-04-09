using System;
using Godot;

namespace EcsR3.Godot.Examples.Asteroids.Services;

public class GameTextureResources : IDisposable
{
    public Texture2D ShipTexture { get; }
    public Texture2D MeteorTexture { get; }
    public Texture2D LaserTexture { get; }
        
    public GameTextureResources()
    {
        ShipTexture = ResourceLoader.Load<Texture2D>("res://Examples/Asteroids/Resources/ship.png");
        MeteorTexture = ResourceLoader.Load<Texture2D>("res://Examples/Asteroids/Resources/meteor.png");
        LaserTexture = ResourceLoader.Load<Texture2D>("res://Examples/Asteroids/Resources/laser.png");
    }

    public void Dispose()
    {
        ShipTexture?.Dispose();
        MeteorTexture?.Dispose();
        LaserTexture?.Dispose();
    }
}