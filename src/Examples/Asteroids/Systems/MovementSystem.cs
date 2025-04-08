using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Groups;
using EcsR3.Godot.Plugins.EcsR3.Godot.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using Godot;
using Godot.DependencyInjection.Services.Input;
using SystemsR3.Plugins.Transforms.Extensions;
using SystemsR3.Scheduling;

namespace EcsR3.Godot.Examples.Asteroids.Systems
{
    public class MovementSystem : IBasicEntitySystem
    {
        public IGroup Group { get; } = new ShipGroup();

        public ITimeTracker TimeTracker { get; }
        public IInputService InputService { get; }

        public MovementSystem(ITimeTracker timeTracker, IInputService inputService)
        {
            TimeTracker = timeTracker;
            InputService = inputService;
        }
        
        public void Process(IEntity entity, ElapsedTime elapsedTime)
        {
            var forwardChange = 0.0f;
            var strafeChange = 0.0f;
            var rotationChange = 0.0f;
            
            if (InputService.IsKeyPressed(Key.W))
            { forwardChange = 1; }
            else if (InputService.IsKeyPressed(Key.S))
            { forwardChange = -1;}
            
            if (InputService.IsKeyPressed(Key.A))
            { strafeChange = -1; }
            else if (InputService.IsKeyPressed(Key.D))
            { strafeChange = 1; }
            
            if (InputService.IsKeyPressed(Key.Q))
            { rotationChange = -1; }
            else if (InputService.IsKeyPressed(Key.E))
            { rotationChange = 1; }
            
            if(forwardChange == 0 && strafeChange == 0 && rotationChange == 0) { return; }
            
            var moveableComponent = entity.GetComponent<MoveableComponent>();
            var movementSpeed = moveableComponent.MovementSpeed * (float)TimeTracker.ElapsedTime.DeltaTime.TotalSeconds;

            var transformComponent = entity.GetComponent<Transform2DComponent>();
            var transform = transformComponent.Transform;
            transform.Position += transform.Forward() * forwardChange * movementSpeed;
            transform.Position += transform.Right() * strafeChange * movementSpeed;
            transform.Rotation += rotationChange * movementSpeed * 0.05f;

            var viewComponent = entity.GetComponent<ViewComponent>();
            viewComponent.SetTransform(transform);
        }
    }
}