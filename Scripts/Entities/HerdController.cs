using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class HerdController : Node2D
{
	LinkedList<HerdMember> AllMembers;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AllMembers = new LinkedList<HerdMember>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void addMember(ref HerdMember member)
	{
		AllMembers.AddLast()
	}
}
