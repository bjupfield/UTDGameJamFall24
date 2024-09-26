extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	##################Map Input
	
	var w = InputEventKey.new()
	var a = InputEventKey.new()
	var s = InputEventKey.new()
	var d = InputEventKey.new()
	w.keycode = KEY_W
	a.keycode = KEY_A
	s.keycode = KEY_S
	d.keycode = KEY_D
	InputMap.add_action("Pressed_W")
	InputMap.action_add_event("Pressed_W", w)
	InputMap.add_action("Pressed_A")
	InputMap.action_add_event("Pressed_A", a)
	InputMap.add_action("Pressed_S")
	InputMap.action_add_event("Pressed_S", s)
	InputMap.add_action("Pressed_D")
	InputMap.action_add_event("Pressed_D", d)
	
	########################Mapped
	
	
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
