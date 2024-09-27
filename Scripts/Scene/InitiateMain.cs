using Godot;
using System;

public partial class InitiateMain : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	//Map Input
	
	InputEventKey w = new InputEventKey();
	InputEventKey a = new InputEventKey();
	InputEventKey s = new InputEventKey();
	InputEventKey d = new InputEventKey();
	w.Keycode = Key.W;
	a.Keycode = Key.A;
	s.Keycode = Key.S;
	d.Keycode = Key.D;
	InputMap.AddAction("Pressed_W");
	InputMap.ActionAddEvent("Pressed_W", w);
	InputMap.AddAction("Pressed_A");
	InputMap.ActionAddEvent("Pressed_A", a);
	InputMap.AddAction("Pressed_S");
	InputMap.ActionAddEvent("Pressed_S", s);
	InputMap.AddAction("Pressed_D");
	InputMap.ActionAddEvent("Pressed_D", d);
	
	//Mapped

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
