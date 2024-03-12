using Godot;
using System;

public partial class Door : Area2D
{
	[Export]
	public int ConnectedRoomID;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		ConnectedRoomID = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void OnBodyEntered(Node2D entity)
	{
		EmitSignal("Hit");
		GD.Print("Go to Room");
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
