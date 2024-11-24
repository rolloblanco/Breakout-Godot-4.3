using Godot;
using System;
using Godot.Collections;

public partial class Main : Node2D
{
	public Label ScoreLabel;
	public Label LivesLabel;
	public Label GameOverLabel;

	[Export] public int BlocksCount = 20;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScoreLabel = GetNode<Label>("Score");
		LivesLabel = GetNode<Label>("Lives");
		GameOverLabel = GetNode<Label>("GameOver");
		ScoreLabel.Text = "0";
		LivesLabel.Text = "3";
		GameOverLabel.Visible = false;
		
		Vector2 viewportSize = GetViewportRect().Size;
		float viewportWidth = viewportSize.X;
		float viewportHeight = viewportSize.Y;
		
		Block tempBlock = Block.MyScene.Instantiate<Block>();
		var blockSize = tempBlock.GetChild<CollisionShape2D>(1).Shape.GetRect().Size;
		var blockWidth = blockSize.X;
		var blockHeight = blockSize.Y;
		tempBlock.QueueFree();
		
		// add blocks to the scene using Utils.ViewportSlicer and random functions
		Array<Dictionary<string, float>> viewportBlocks = Utils.ViewportSplicer(viewportWidth, viewportHeight/2, blockWidth, blockHeight);
		viewportBlocks.Shuffle(); // randomise it
		viewportBlocks.Slice(0, BlocksCount); // take the amount of blocks required

		for (int i = 0; i < BlocksCount; i++)
		{
			var blockCoords = viewportBlocks[i];
			Block block = Block.CreateWithParameters(GD.RandRange(1, 3));
			
			Vector2 position = new Vector2(
				blockCoords["X"],
				blockCoords["Y"]);
			block.Position = position;
			block.BlockDestroyed += OnBlockDestroyed;
			AddChild(block);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape))
		{
			Scenes.Instance.SwitchScene(Scenes.Instance.MenuScene);
		}
	}

	public void OnBlockDestroyed()
	{
		ScoreLabel.Text = (ScoreLabel.Text.ToInt() + 1).ToString();
		BlocksCount--;

		if (BlocksCount == 0)
		{
			GameOverLabel.Text = "You Win!";
			GameOverLabel.Visible = true;
			GetTree().Paused = true;
		}
	}

	public async void OnBallEscaped(Ball ball)
	{
		GD.Print("Ball escaped");
		LivesLabel.Text = (LivesLabel.Text.ToInt() - 1).ToString();
		if (LivesLabel.Text == "0")
		{
			GameOverLabel.Text = "You Lose!";
			GameOverLabel.Visible = true;
			GetTree().Paused = true;
		}
		
		ball.Position = ball.StartPosition;
		ball.Direction = Vector2.Zero;
		await ToSignal(GetTree().CreateTimer(3), "timeout");
		ball.Speed = Ball.BASE_SPEED;
		ball.Direction = Vector2.Down;
	}
}
