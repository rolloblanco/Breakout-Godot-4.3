using Godot;
using System;

public partial class Scenes : Node
{
	[Export] public PackedScene MenuScene;
	[Export] public PackedScene GameScene;
	[Export] public int hello = 2;
	
	public Node CurrentScene;
	
	public static Scenes Instance { get; private set; } // allows reference to Data without node path
	
	public override void _Ready()
	{
		Instance = this;
		
		var root = GetTree().Root;
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SwitchScene(PackedScene scene)
	{
		GetTree().ChangeSceneToPacked(scene);
	}
}
