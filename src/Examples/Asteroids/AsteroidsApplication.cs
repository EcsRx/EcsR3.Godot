using EcsR3.Godot.Examples.Asteroids.Blueprints;
using EcsR3.Godot.Examples.Asteroids.Modules;
using EcsR3.Godot.Examples.Asteroids.Services;
using EcsR3.Godot.Plugins.EcsR3.Godot.Applications;
using SystemsR3.Infrastructure.Extensions;

namespace EcsR3.Godot.Examples.Asteroids;

public partial class AsteroidsApplication : GodotApplication
{
	public GameTextureResources GameTextureResources { get; set; }

	protected override void ResolveApplicationDependencies()
	{
		base.ResolveApplicationDependencies();
		GameTextureResources = DependencyResolver.Resolve<GameTextureResources>();
	}

	public override void StopApplication()
	{
		base.StopApplication();
		GameTextureResources.Dispose();
	}

	protected override void LoadModules()
	{
		base.LoadModules();
		DependencyRegistry.LoadModule<ExampleGameModule>();
	}

	protected override void ApplicationStarted()
	{
		var defaultCollection = EntityDatabase.GetCollection();
		defaultCollection.CreateEntity(new ShipBlueprint());
	}
}
