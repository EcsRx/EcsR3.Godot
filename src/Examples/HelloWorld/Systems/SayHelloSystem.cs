using System;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.HelloWorld.Components;
using EcsR3.Groups;
using EcsR3.Groups.Observable;
using EcsR3.Systems;
using Godot;
using R3;

namespace EcsR3.Godot.Examples.HelloWorld.Systems
{
	public partial class SayHelloSystem : IReactToGroupSystem
	{
		public IGroup Group { get; } = new Group(typeof(SayHelloComponent));
		
		public Observable<IObservableGroup> ReactToGroup(IObservableGroup observableGroup)
		{ return Observable.Interval(TimeSpan.FromSeconds(1)).Select(x => observableGroup); }

		public void Process(IEntity entity)
		{
			var helloComponent = entity.GetComponent<SayHelloComponent>();
			GD.Print($"Hello there {helloComponent.Name} @ {DateTime.Now}");
		}
	}
}
