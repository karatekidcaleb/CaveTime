using Godot;
using System;


public partial class test_scene : Node
{
	[Export]
	public Chain chain;

	private PackedScene roomScene;
	
	public override void _Ready()
	{
		LoadScenes();
	}

	public void LoadScenes()
	{
		//load the initial room
		roomScene = GD.Load<PackedScene>("res://room.tscn");
		room firstRoom = (room)roomScene.Instantiate();
		AddChild(firstRoom);

		//instantiate the player object
		PackedScene playerScene = GD.Load<PackedScene>("res://player.tscn");
		player player = (player)playerScene.Instantiate();
		AddChild(player);

		//instantiate the tether object
		PackedScene tetherScene = GD.Load<PackedScene>("res://tether.tscn");
		Tether tether = (Tether)tetherScene.Instantiate();
		AddChild(tether);

		PackedScene chainScene = GD.Load<PackedScene>("res://chain.tscn");
		Chain chain = (Chain)chainScene.Instantiate();
		AddChild(chain);

		chain.SetAnchors(player, tether);
		chain.InitializeChain();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void OnChainUpdate()
	{
		
	}
}
