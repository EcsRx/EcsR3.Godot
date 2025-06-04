using EcsR3.Components.Database;
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
            var computedDatabase = resolver.Resolve<IComponentDatabase>();
            var computedComponentGroup =
                resolver.ResolveComputedComponentGroup<ColliderComponent, Transform2DComponent>();
            return new ComputedRuntimeColliders(computedDatabase, computedComponentGroup);
        }));
    }
}