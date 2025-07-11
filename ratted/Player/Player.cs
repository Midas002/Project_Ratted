using Godot;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody3D
{
    [Export]
    private int speed = 15;
    [Export]
    private int fallAcceleration { get; set; } = 75;

    private Vector3 _targetVelocity = Vector3.Zero;
    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector3.Zero;
        if (Input.IsActionPressed("forward"))
        {
            direction.Z = 1.0f;
        }
        if (Input.IsActionPressed("backward"))
        {
            direction.Z = -1.0f;
        }
        if (Input.IsActionPressed("left"))
        {
            direction.X = 1.0f;
        }
        if (Input.IsActionPressed("right"))
        {
            direction.X = -1.0f;
        }

        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            //GetNode<CharacterBody3D>("Player").Basis = Basis.LookingAt(direction);
        }

        _targetVelocity.X = direction.X * speed;
        _targetVelocity.Z = direction.Z * speed;

        if (!IsOnFloor())
        {
            _targetVelocity.Y -= fallAcceleration * (float)delta;
        }

        Velocity = _targetVelocity;
        MoveAndSlide();
    }
}
