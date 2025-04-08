using EcsR3.Components;

namespace EcsR3.Godot.Examples.HelloWorld.Components
{
	public partial class SayHelloComponent : IComponent
	{
		public string Name { get; set; }
	}
}
