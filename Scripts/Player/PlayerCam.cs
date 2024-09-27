using Godot;
using System;

[Signal]
public delegate void Camera_MoveEventHandler(Vector2 moveTo, Vector2 cameraPos, Vector2 playerPos);
public partial class PlayerCam : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _Move_Camera(Vector2 moveTo, Vector2 playerOff)
	{
		this.Position = moveTo;
		EmitSignal("Camera_Move", moveTo, this.Position, playerOff);
	}
}
