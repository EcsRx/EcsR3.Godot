using EcsR3.Godot.Examples.HelloWorld.Components;
using EcsR3.Godot.Plugins.EcsR3.Godot.Applications;
using EcsR3.Extensions;

namespace EcsR3.Godot.Examples.HelloWorld
{
	public partial class Application : GodotApplication
	{
		protected override void ApplicationStarted()
		{
			var collection = EntityDatabase.GetCollection();
			var entity = collection.CreateEntity();

			var helloComponent = new SayHelloComponent
			{
				Name = "Bob"
			};
			entity.AddComponent(helloComponent);
		}
	}
}
