using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Chain : Node2D
{

	[Export]
	public Area2D Tether  { get; set; }

	[Export]
	public player Player  { get; set; }

	[Export]
	public int ChainLinkSize { get; set; }

	[Export]
	public double LengthOfChain  { get; set; }

	[Export]
	public float Chain_Weight { get; set; }

	[Export]
	public PackedScene AllLinksScene { get; set; }

	public List<Chain_link> links;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ChainLinkSize = 24;
		AllLinksScene = GD.Load<PackedScene>("res://Chain_link.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//MoveLinks();
	}

	public void SetAnchors(player player, Area2D tether)
	{
		this.Tether = tether;
		this.Player = player;
	}

	public void MoveLinks()
	{
		int numOfLinks = 0;
		LengthOfChain = Math.Sqrt(Math.Pow(Player.Position.X - Tether.Position.X, 2) + Math.Pow(Player.Position.X - Tether.Position.X, 2));
		numOfLinks = (int)(LengthOfChain / ChainLinkSize) + 1;
		if (links.Count != numOfLinks)
		{
			
		}

		for(int i = links.Count-1; i > 0; i--)
		{
			/*
			if(i != 0 && links[i].colliders.Count <=2)
			{
				links[i].Position += 0.1f*(links[i-1].Position - links[i].Position);
			}
			else
			{
				links[i].Position = links[i].Position * (float)0.8 + (float)0.2 * (Tether.Position + (Player.Position - Tether.Position) * ((float)i / (float)(LengthOfChain / ChainLinkSize)));
			}
			Originally this code was intended to loop through and perform operations on each chain link.... but who better to perform movements on individual chain link operatiosn than the links themselves??
			*/
		}	
		GD.Print("hi we got here too");
	}

	public void InitializeChain()
	{
		links = new List<Chain_link>();
		LengthOfChain =  Math.Sqrt(Math.Pow(Player.Position.X - Tether.Position.X, 2) + Math.Pow(Player.Position.X - Tether.Position.X, 2));
		int numOfChains = (int)(LengthOfChain / ChainLinkSize) + 1;

		

		for(int i = 0; i< numOfChains; i++)
		{
			Chain_link link = (Chain_link)AllLinksScene.Instantiate();
			link.Position = Tether.Position + (Player.Position - Tether.Position) * ((float)i / (float)numOfChains);
			if (i != 0)
			{
				link.From_Object = links[i-1];
				links[i-1].To_Object = link;
			}
			else
				link.From_Object = this.Tether;
			links.Add(link);
			AddChild(link);
		}
		links[numOfChains-1].To_Object = this.Player;
		this.Player.chain = links[numOfChains-1];
		this.Player.chained = true;
	}
}
