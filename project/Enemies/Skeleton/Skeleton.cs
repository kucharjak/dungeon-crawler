using Godot;
using System;
using DungeonCrawler.Combat;
using DungeonCrawler.Enemies;
using DungeonCrawler.Extensions;

public class Skeleton : KinematicBody2D
{
    // [Export] public string DisplayName = "Bones";
    
    [Export] public int MaxSpeed = 80;
    [Export] public int Acceleration = 100;
    [Export] public int Friction = 100;

    [Export] public bool IsAggressive = true;

    protected Vector2 Velocity = Vector2.Zero;
    
    protected DetectionZone DetectionZone;
    protected Node2D Target = null;
    
    protected EnemyState State = EnemyState.Follow;
    
    public override void _Ready()
    {
        // DetectionZone = this.GetChildNode<DetectionZone>();
        // DetectionZone.Connect("TargetSpotted", )
    }

    public override void _PhysicsProcess(float delta)
    {
        switch (State)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Follow:
                ProcessFollow(delta);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    protected void ProcessIdle(float delta)
    {
        
    }

    protected void ProcessFollow(float delta)
    {
        if (Target is null)
            return;

        var targetDirection = Target.Position - Position;
        targetDirection = targetDirection.Normalized();

        MoveAndSlide(targetDirection * MaxSpeed);
    }

    public void OnTargetSpotted(Node2D target)
    {
        Target = target;
    }
    
    public void OnTargetLost(Node2D target)
    {
        Target = null;
    }
}
