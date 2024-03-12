using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Chain_link : Area2D
{

	[Export]
	public int Chain_Link_Length = 18;

	[Export]
	public float Chain_Momentum = 0.80f;

	[Export]
	public Area2D From_Object;

	[Export]
	public Area2D To_Object;

	[Export]
	public Vector2 velocity;

	[Export]
	public float Speed = 100;

	public List<Area2D> colliders;

	public override void _Ready()
	{
		colliders = new List<Area2D>();
		ZIndex = 2;
	}

	public override void _Process(double delta)
	{
		/* this works but really shitty
		if(To_Object.Position.DistanceTo(Position) > Chain_Link_Length)
		{
			velocity = To_Object.Position - Position;
			velocity = velocity.Normalized() * Speed;
			Position += velocity * (float) delta;
		}
		*/
		
		// git push "try again" > bitchAttempt.txt
		Position = Position * (1-Chain_Momentum) + Chain_Momentum * (To_Object.Position + From_Object.Position) / 2;
	}

	public void OnAreaEntered(Area2D area)
	{
		colliders.Append<Area2D>(area);
		if(area != To_Object && area != From_Object)
		{
			
		}
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body is Chain_link)
		{
			
		}
	}
}
