using Godot;
using System;

public partial class Menu : Control
{
	[Export] public Node Game;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("Start").GrabFocus();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnStartPressed()
	{
		Scenes.Instance.SwitchScene(Scenes.Instance.GameScene);
	}
	
	public void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
