using Godot;
using System;

public partial class Tether : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = new Vector2(
		x: (float)GD.RandRange(50.0f, 400f),
		y: (float)GD.RandRange(50.0f, 400f)
	);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
