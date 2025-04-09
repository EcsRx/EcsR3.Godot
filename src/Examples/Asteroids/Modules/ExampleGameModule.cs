using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Computed;
using EcsR3.Godot.Examples.Asteroids.Services;
using EcsR3.Infrastructure.Extensions;
using EcsR3.Plugins.Transforms.Components;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;

namespace EcsR3.Godot.Examples.Asteroids.Modules;

public class ExampleGameModule : IDependencyModule
{
    public void Setup(IDependencyRegistry registry)
    {
        registry.Bind<GameTextureResources>();
        
        registry.Bind<ComputedRuntimeColliders>(x => x.ToMethod(resolver =>
        {
            var observableGroup =
                resolver.ResolveObservableGroup(typeof(ColliderComponent), typeof(Transform2DComponent));
            return new ComputedRuntimeColliders(observableGroup);
        }));
    }
}