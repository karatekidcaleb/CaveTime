using Godot;
using System;

public partial class player : Area2D
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void HitEventHandler();

	[Export]
	public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).
	[Export]
	public bool chained;

	[Export]
	public Chain_link chain;

	public Vector2 ScreenSize; // Size of the game window.

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Vector2 start_position = new(
		x: (float)GD.RandRange(50.0f, (float)(ScreenSize.X - 50)),
		y: (float)GD.RandRange(50.0f, (float)(ScreenSize.Y - 50))
	);
		Start(start_position);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
			var velocity = Vector2.Zero; // The player's movement vector.

	if (Input.IsActionPressed("move_right"))
	{
		velocity.X += 1;
	}

	if (Input.IsActionPressed("move_left"))
	{
		velocity.X -= 1;
	}

	if (Input.IsActionPressed("move_down"))
	{
		velocity.Y += 1;
	}

	if (Input.IsActionPressed("move_up"))
	{
		velocity.Y -= 1;
	}

	var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

	if (velocity.Length() > 0)
	{
		velocity = velocity.Normalized() * Speed;
		animatedSprite2D.Play();
	}
	else
	{
		animatedSprite2D.Stop();
	}
	Position += velocity * (float)delta;
	Position = new Vector2(
		x: Mathf.Clamp(Position.X, 20, ScreenSize.X-20),
		y: Mathf.Clamp(Position.Y, 20, ScreenSize.Y-20)
	);
	if(chained)
	{ //TODO: the math to make this bounding square into a bounding radius. also TODO: buy gun to kill self
		Position = new Vector2(
		x: Mathf.Clamp(Position.X, chain.Position.X-30.0f, chain.Position.X+30.0f),
		y: Mathf.Clamp(Position.Y, chain.Position.Y-30.0f, chain.Position.Y+30.0f)
	);
	}

	if (velocity.X != 0)
	{
		animatedSprite2D.Animation = "walk";
		animatedSprite2D.FlipV = false;
		// See the note below about boolean assignment.
		animatedSprite2D.FlipH = velocity.X < 0;
	}
	else if (velocity.Y != 0)
	{
		if(velocity.Y > 0)
		{
			animatedSprite2D.Animation = "up";
		}
		else
		{
			animatedSprite2D.Animation = "down";
		}
	}
	}
	
	//signals
	private void OnBodyEntered(Node2D body)
	{
		Hide(); // Player disappears after being hit.
		EmitSignal(SignalName.Hit);
		GD.Print("hi");
		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

	public void OnHit()
	{
		GD.Print("hi");
	}

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		ZIndex = 1;
    }
}

