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
	public override void _Process(double delta)
	{
		if(Input.IsKeyPressed(Key.Left))
		{
			float deltaFloat = (float)delta;
			Velocity = new Vector2(-SPEED, 0);
		}
		else if (Input.IsKeyPressed(Key.Right))
		{
			float deltaFloat = (float)delta;
			Velocity = new Vector2(SPEED, 0);
		}
		else
		{
			Velocity = Vector2.Zero;
		}

		MoveAndSlide();
	}

	public void OnBallHit(Ball ball, KinematicCollision2D contact)
	{
		var velocity = ball.GlobalPosition - GlobalPosition;

		ball.Direction = (velocity.Normalized() + ball.Direction.Bounce(contact.GetNormal())) / 2;
	}
}
