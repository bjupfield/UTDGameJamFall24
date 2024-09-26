extends Camera2D

signal camera_move(moveTo, cameraPosition, playerOffset)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:

	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:

	pass

func _move_camera(moveTo, playerOffset) -> void:
	self.position = moveTo;
	camera_move.emit(moveTo, self.position, playerOffset)
	pass

func _resize_camera(window_size) -> void:
	
	pass
