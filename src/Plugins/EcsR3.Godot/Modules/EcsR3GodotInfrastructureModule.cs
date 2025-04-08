using EcsR3.Godot.Plugins.EcsR3.Godot.Scheduler;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Scheduling;

namespace EcsR3.Godot.Plugins.EcsR3.Godot.Modules;

public class EcsR3GodotInfrastructureModule : IDependencyModule
{
    public void Setup(IDependencyRegistry registry)
    {
        registry.Unbind<IUpdateScheduler>();
        registry.Bind<IUpdateScheduler, GodotUpdateScheduler>();
    }
}