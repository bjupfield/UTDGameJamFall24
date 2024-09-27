using Godot;
using System;
using System.Runtime.CompilerServices;


public struct HerdMemberReturnStruct
{
	Vector2 newPos;
	Vector2 moveVec;
	float divergence;
	int memberId;
}


public unsafe struct herdMemberMapData
{ 
//***********************************************/
//This Data Will be placed in below HerdBlobStruct in a Map
//
/***********************************************/

	public Vector2 movVec;
	public Vector2 realPos;
	public void* func;
	int blobId;
	herdMemberMapData* next;
}
public struct HerdBlobStruct
{

	Vector2 blobPos;
	Vector2 blobVec;
}
public unsafe struct herdMemberSynchronizedData
{
/************************************/
//This Contains info that will be passed to all herd members and calculated at the same time
/************************************/
	herdMemberMapData[,]* previousMap;
	herdMemberMapData[,]* newMap;
	HerdBlobStruct[] blobInfo;
	
	float preMapStepValue;
	float postMapStepValue;
}
