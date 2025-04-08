using Godot;

namespace EcsR3.Godot.Examples.Asteroids.Services;

public class GameTextureResources
{
    public Texture2D ShipTexture { get; }
        
    public GameTextureResources()
    {
        ShipTexture = ResourceLoader.Load<Texture2D>("res://Examples/Asteroids/Resources/ship.png");
    }
}