using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class HerdController : Node2D
{
	private int herdIDCount;
	private LinkedList<HerdMember> members;

	private HerdBlobStruct[] preBlobs;
	private HerdBlobStruct[] nextBlobs;
	private HerdMemberMapData[,] previousMap;
	private HerdMemberMapData[,] nextMap;
	private Vector2 preMapOffset;
	private Vector2 postMapOffset;
	private HerdMemberSynchronizedData controlData;

	// Called when the node enters the scene tree for the first time.ex
	public override void _Ready()
	{
		herdIDCount = 0;
		members = new LinkedList<HerdMember>();
		preBlobs = new HerdBlobStruct[0];
		nextBlobs = new HerdBlobStruct[0];
		previousMap = new HerdMemberMapData[1000,1000];
		nextMap = new HerdMemberMapData[1000,1000];
		preMapOffset = Vector2.Zero;
		postMapOffset = Vector2.Zero;
		fillPassData();
	}
	private unsafe void fillPassData()
	{
		controlData = new HerdMemberSynchronizedData();
		fixed (HerdMemberMapData[,]* b = &previousMap)
		{
			controlData.preMap = b;
		}
		fixed (HerdMemberMapData[,]* b = &nextMap)
		{
			controlData.nextMap = b;
		}
		fixed (HerdBlobStruct[]* b = &preBlobs)
		{
			controlData.preBlobs= b;
		}
		fixed (HerdBlobStruct[]* b = &nextBlobs)
		{
			controlData.nextBlobs = b;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public int addMember(HerdMember member)
	{
		members.AddLast(member);
		return herdIDCount++;
	}
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		
    }
}
