using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Godot.Examples.Asteroids.Components;
using EcsR3.Godot.Examples.Asteroids.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
using Godot;
using SystemsR3.Attributes;
using SystemsR3.Scheduling;
using SystemsR3.Types;

namespace EcsR3.Godot.Examples.Asteroids.Systems.Infrastructure;

[Priority(PriorityTypes.SuperLow)]
public class ScoreDrawingSystem : IBasicEntitySystem
{
    public IGroup Group { get; } =  new Group(typeof(PlayerComponent));
    public Label ScoreLabel { get; set; } 
        
    public ScoreDrawingSystem()
    {
       this.RunOnGodotThread(() => ScoreLabel = this.GetRootScene().CurrentScene.FindChild("ScoreValueLabel") as Label);
    }

    public void Process(IEntity entity, ElapsedTime elapsedTime)
    {
        if(ScoreLabel == null) { return; }
        
        var playerComponent = entity.GetComponent<PlayerComponent>();
        ScoreLabel.Text = playerComponent.Score.ToString();
    }
}