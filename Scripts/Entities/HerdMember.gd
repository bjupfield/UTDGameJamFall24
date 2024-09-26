extends ColorRect

var timer
var tick = 0
var back = false
var rng = RandomNumberGenerator.new()

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	timer = Time.get_ticks_msec()
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	timer = Time.get_ticks_msec()
	if(!back && tick < 4):
		for x in range(0, 3):
			var thread = Thread.new()
			thread.start(_printRandTime.bind(timer))
		tick += 1
		back = !back
	if(back && timer % 2000 > 1000):
		back = !back
	pass

func _printRandTime(startTime: float) -> void:
	for x in range(0, rng.randi_range(100000, 800000)):
		pass
	print_debug("Call Time:" + str(startTime) + " || " + str(Time.get_ticks_msec()))
	pass
