using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Groups;
using EcsR3.Godot.Examples.Asteroids.Services;
using EcsR3.Godot.Plugins.EcsR3.Godot.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using Godot;
using Vector2 = System.Numerics.Vector2;

namespace EcsR3.Godot.Examples.Asteroids.Systems
{
    public class ShipSetupComponent : ISetupSystem
    {
        public IGroup Group { get; } = new ShipGroup();
        
        public GameTextureResources GameTextureResources { get; }

        public ShipSetupComponent(GameTextureResources gameTextureResources)
        {
            GameTextureResources = gameTextureResources;
        }
        
        public void Setup(IEntity entity)
        {
            var transformComponent = entity.GetComponent<Transform2DComponent>();
            transformComponent.Transform.Position = new Vector2(100, 100);

            var movableComponent = entity.GetComponent<MoveableComponent>();
            movableComponent.MovementSpeed = 100.0f;

            var viewComponent = entity.GetComponent<ViewComponent>();
            
            var sprite = new Sprite2D();
            sprite.Texture = GameTextureResources.ShipTexture;
            viewComponent.View = sprite;
            viewComponent.SetTransform(transformComponent.Transform);

            var sceneTree = (SceneTree)Engine.GetMainLoop();
            Callable.From(() => sceneTree.Root.AddChild(sprite)).CallDeferred();
        }
    }
}