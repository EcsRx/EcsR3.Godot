using EcsR3.Godot.Examples.Asteroids.Blueprints;
using EcsR3.Godot.Examples.Asteroids.Modules;
using EcsR3.Godot.Plugins.EcsR3.Godot.Applications;
using SystemsR3.Infrastructure.Extensions;

namespace EcsR3.Godot.Examples.Asteroids;

public partial class ShipApplication : GodotApplication
{
	protected override void LoadModules()
	{
		base.LoadModules();
		DependencyRegistry.LoadModule<ExampleGameModule>();
	}

	protected override void ApplicationStarted()
	{
		var defaultCollection = EntityDatabase.GetCollection();

		var shipSpriteNode = GetParent().FindChild("Ship");
		var shipEntity = defaultCollection.CreateEntity(new ShipBlueprint());
		
//		shipEntity.GetComponent<ViewComponent>().View = shipSpriteNode;
	}
	
	
}
