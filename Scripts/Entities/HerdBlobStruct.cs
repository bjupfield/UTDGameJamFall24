using Godot;
using System;
using System.Runtime.CompilerServices;


public unsafe struct HerdMemberMapData
{ 
//***********************************************/
//This Data Will be placed in below HerdBlobStruct in a Map
//
/***********************************************/

	public Vector2 movVec;
	public Vector2 realPos;
	public int blobId;
	public int memberId;
	public HerdMemberMapData* next;
}
public struct HerdBlobStruct
{

	public Vector2 blobPos;
	public Vector2 blobVec;
	public int memberCount;
}
public unsafe struct HerdMemberSynchronizedData
{
/************************************/
//This Contains info that will be passed to all herd members and calculated at the same time
/************************************/
	public HerdMemberMapData[,]* preMap;
	public HerdMemberMapData[,]* nextMap;
	public HerdBlobStruct[]* preBlobs;
	public HerdBlobStruct[]* nextBlobs;
	
	public float preMapStepValue;
	public float nextMapStepValue;
	public Vector2 preMapOffset;
	public Vector2 nextMapOffset;
}
