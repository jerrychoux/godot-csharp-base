using Godot;
using System;

public class Enemy : RigidBody2D
{

    [Export]
    public int minSpeed = 150;
    [Export]
    public int maxSpeed = 250;

    static private Random _random = new Random();

    public override void _Ready()
    {
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        var animationNames = animatedSprite.Frames.GetAnimationNames();
        animatedSprite.Animation = animationNames[_random.Next(0, animationNames.Length)];
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
