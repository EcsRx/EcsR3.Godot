using System;
using EcsR3.Computeds.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.HelloWorld.Components;
using EcsR3.Groups;
using EcsR3.Systems.Reactive;
using Godot;
using R3;

namespace EcsR3.Godot.Examples.HelloWorld.Systems
{
	public partial class SayHelloSystem : IReactToGroupSystem
	{
		public IGroup Group { get; } = new Group(typeof(SayHelloComponent));
		
		public Observable<IComputedEntityGroup> ReactToGroup(IComputedEntityGroup computedEntityGroup)
		{ return Observable.Interval(TimeSpan.FromSeconds(1)).Select(x => computedEntityGroup); }

		public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
		{
			var helloComponent = entityComponentAccessor.GetComponent<SayHelloComponent>(entity);
			GD.Print($"Hello there {helloComponent.Name} @ {DateTime.Now}");
		}
	}
}
