using Godot;
using System;

namespace CovenWWDC23Port.Scenes;

public partial class Danmaku : Node2D
{
	private Texture2D _texture;
	private SpriteSpinEnum _spriteSpin = SpriteSpinEnum.None;
	private bool _ownerIsPlayer;

	// Arrays
	private List<Bullet> _bulletArray = new List<Bullet>();
	private int _patternArrays = 1;
	private int _bulletsPerArray = 1;

	// Angle Variables
	private int _spreadBetweenArray = 300;
	private int _spreadWithinArray = 90;
	private float _startAngle = 0.0f;
	private float _defaultAngle = 0.0f;

	// Spinning Variables
	private float _spinRate = 0.0f;
	private float _spinModificator = 0.0f;
	private bool _invertSpin = true;
	private float _maxSpinRate = 10.0f;

	// Fire Rate Variables
	private int _fireRate = 5;
	private int _shoot = 0;

	// Offsets
	private float _objectWidth = 0.0f;
	private float _objectHeight = 0.0f;
	private float _xOffset = 0.0f;
	private float _yOffset = 0.0f;

	// Bullet Variables
	private float _bulletSpeed = 3.0f;
	private float _bulletAcceleration = 0.0f;
	private float _bulletCurve = 0.0f;
	private int _bulletTtl = 10;

	// Clear Timer
	private System.Threading.Timer _timer;
	private double _time = 0;
	private const double TimePeriod = 1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

public enum SpriteSpinEnum
{
	None = 0,
	Clockwise = -345,
	CounterClockwise = 345
}