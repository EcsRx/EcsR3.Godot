using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Godot.Plugins.EcsR3.Godot.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using SystemsR3.Attributes;
using SystemsR3.Scheduling;
using SystemsR3.Types;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Infrastructure;

[Priority(PriorityTypes.SuperLow)]
public class SyncTransformSystem : IBasicEntitySystem
{
    public IGroup Group { get; } = new Group(typeof(ViewComponent), typeof(Transform2DComponent));
    
    public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity, ElapsedTime elapsedTime)
    {
        var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
        var transformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        
        if(viewComponent.View != null)
        { viewComponent.SetTransform(transformComponent.Transform); }
    }
}