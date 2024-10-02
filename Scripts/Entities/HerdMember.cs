using Godot;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

public partial class HerdMember : ColorRect
{
[DllImport("./HerdManager/bin/HerdContr.so", EntryPoint ="func")]
static extern int func();

	[Export]
	public HerdController herdCont {get; set;}
	[Export]
	public float desiredSpeed {get; set;}
	[Export]
	public float maxSpeed {get; set;}

	[Export]
	public float memberToRadius {get; set;}

	public HerdMemberMapData memberData;

	private Label text;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int memberID = herdCont.addMember(this);
		memberData = new HerdMemberMapData()
		{
			movVec = Vector2.Zero,
			realPos = this.Position,
			blobId = -1,
			memberId = memberID,
			next = null,
		};
		text = (Label)this.FindChild("Label");
		//text.Set("text", memberID.ToString());
		text.Set("text", func());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 b = new Vector2(func(), func());
		this.Position = b;
	}

	public unsafe void move_member(HerdMemberSynchronizedData data)
	{
		int radiusCheck;
		float preRadiusSpeedCalc = desiredSpeed;
		for(int i = 0; i < (*data.preBlobs).Length; i++)
		{
			//weight the blob vectors and mix them with the desired speed 
			float blobDist = (this.memberData.realPos - (*data.preBlobs)[i].blobPos).Length();
			blobDist /= memberToRadius;
			blobDist = Mathf.Max(1 / (blobDist * blobDist), 1f);
			
		}
	}
}
