using EcsR3.Godot.Examples.Asteroids.Services;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;

namespace EcsR3.Godot.Examples.Asteroids.Modules;

public class ExampleGameModule : IDependencyModule
{
    public void Setup(IDependencyRegistry registry)
    {
        registry.Bind<GameTextureResources>();
    }
}