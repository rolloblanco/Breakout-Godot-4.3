using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Signal] public delegate void BallEscapedEventHandler(Ball ball);
	
	public const float BASE_SPEED = 100f;
	public float Speed = 100f;
	public const float SPEED_MULTIPLIER = 0.025f;
	public Vector2 Direction = Vector2.Zero;
	public Vector2 StartPosition;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartPosition = Position;
		Direction = Vector2.Down;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var collision = MoveAndCollide(Direction.Normalized() * Speed * (float)delta);

		if (collision != null)
		{
			Speed += BASE_SPEED * SPEED_MULTIPLIER;
			GD.Print("Speed: " + Speed);

			if (collision.GetCollider() is Paddle paddle)
			{
				paddle.OnBallHit(this, collision);
				GD.Print("Paddle hit");
			}
			else if (collision.GetCollider() is Block block)
			{
				block.OnBallHit(this, collision);
				GD.Print("Block hit");
			}
			else
			{
				Direction = Direction.Bounce(collision.GetNormal());
				GD.Print("Wall hit");
			}
		}

		if (Position.Y > GetViewportRect().Size.Y)
		{
			EmitSignal("BallEscaped", this);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		
	}
}
