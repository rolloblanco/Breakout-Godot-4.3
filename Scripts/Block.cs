using Godot;
using System;

public partial class Block : StaticBody2D
{
	[Signal] public delegate void BlockDestroyedEventHandler();
	
	public static PackedScene MyScene = GD.Load<PackedScene>("res://block.tscn");

	[Export] public Sprite2D MySprite;

	public int Health = 1; // 1 by default

	public Block()
	{
		
	}

	public Block(int health)
	{
		Health = health;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBallHit(Ball ball, KinematicCollision2D contact)
	{
		var velocity = ball.GlobalPosition - GlobalPosition;
		ball.Direction = (velocity.Normalized() + ball.Direction.Bounce(contact.GetNormal())) / 2;
		Health--;
		SetColor();
		if (Health == 0)
		{
			EmitSignal("BlockDestroyed");
			QueueFree();
		}
	}

	private void SetColor()
	{
		switch (Health)
		{
			case 1:
				MySprite.Modulate = Colors.White;
				break;
			case 2:
				MySprite.Modulate = Colors.LightBlue;
				break;
			case 3:
				MySprite.Modulate = Colors.LightGreen;
				break;
		}
	}

	public static Block CreateWithParameters(int health)
	{
		Block block = MyScene.Instantiate<Block>();
		block.Health = health;
		block.SetColor();
		
		return block;
	}
}
