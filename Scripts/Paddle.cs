using Godot;
using System;

public partial class Paddle : CharacterBody2D
{
	public const float SPEED = 200f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = new Vector2(Input.GetAxis("left", "right"), 0);
		Vector2 velocity = direction * SPEED;
		
		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnBallHit(Ball ball, KinematicCollision2D contact)
	{
		var velocity = ball.GlobalPosition - GlobalPosition;

		ball.Direction = (velocity.Normalized() + ball.Direction.Bounce(contact.GetNormal())) / 2;
	}
}
