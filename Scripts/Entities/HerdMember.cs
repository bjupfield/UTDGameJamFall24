using Godot;
using System;
using System.Linq;

public partial class HerdMember : ColorRect
{

	HerdMemberReturnStruct value;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		((HerdController)GetParent()).
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public unsafe HerdMemberReturnStruct move_member(herdMemberMapData[,]* previousMap)
	{

		//set value to something after 
		herdMemberMapData[,] b = *previousMap;
		(*(*previousMap)[1,2].func)(2);
		HerdBlobStruct[] blobInfo;
		blobInfo.
		return value;
	}
}
