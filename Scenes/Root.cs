using Godot;
using System;

public class Root : Node
{
    [Export]
    public PackedScene Enemy;
    private int _score;
    private Random _random = new Random();
    public override void _Ready()
    {
        GameNew();
    }


    public override void _Process(float delta)
    {

    }

    private float RandRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("EnemyTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    public void OnScoreTimerTimeout()
    {
        _score++;
    }

    public void OnEnemyTimerTimeout()
    {
        var enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
        enemySpawnLocation.Offset = _random.Next();

        var enemyInstance = Enemy.Instance() as RigidBody2D;
        AddChild(enemyInstance);

        float direction = enemySpawnLocation.Rotation + Mathf.Pi / 2;
        enemyInstance.Position = enemySpawnLocation.Position;

        direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        enemyInstance.Rotation = direction;

        enemyInstance.LinearVelocity = new Vector2(RandRange(150f, 250f), 0).Rotated(direction);

    }

    public void GameOver()
    {
        GetNode<Timer>("EnemyTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }

    public void GameNew()
    {
        _score = 0;

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
    }
}
