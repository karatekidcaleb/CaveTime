using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;

public partial class room : Node2D
{
	// Called when the node enters the scene tree for the first time.
	protected int[,] roomArray = {
    {1, 2, 3, 4},
    {5, 6, 7, 8},
    {9, 10, 11, 12},
    {13, 14, 15, 16}
};

	public List<Door> doors;

	public override void _Ready()
	{
		
		LoadDoors(0);
	}

	public void LoadDoors(int RoomID)
	{
		PackedScene doorScene = GD.Load<PackedScene>("res://door.tscn");
		Door westDoor = (Door)doorScene.Instantiate();
		westDoor.Rotate((float)(Math.PI / 2));
		westDoor.Position = Position + new Vector2(0, 300);
		westDoor.ZIndex = 1;
		westDoor.ConnectedRoomID = roomArray[RoomID, 1];
		if(westDoor.ConnectedRoomID != 100)
		{
			AddChild(westDoor);
		}
		Door eastDoor = (Door)doorScene.Instantiate();
		eastDoor.Position = Position + new Vector2(1150, 300);
		eastDoor.Rotate((float)(Math.PI / 2));
		eastDoor.ZIndex = 1;
		eastDoor.ConnectedRoomID = roomArray[RoomID, 2];
		if(westDoor.ConnectedRoomID != 100)
		{
			AddChild(eastDoor);
		}
		Door northDoor = (Door)doorScene.Instantiate();
		northDoor.Position = Position + new Vector2(570, 0);
		northDoor.ZIndex = 1;
		northDoor.ConnectedRoomID = roomArray[RoomID, 0];
		if(westDoor.ConnectedRoomID != 100)
		{
			AddChild(northDoor);
		}
		Door southDoor = (Door)doorScene.Instantiate();
		southDoor.Position = Position + new Vector2(570, 650);
		southDoor.ZIndex = 1;
		southDoor.ConnectedRoomID = roomArray[RoomID, 3];
		if(westDoor.ConnectedRoomID != 100)
		{
			AddChild(southDoor);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
