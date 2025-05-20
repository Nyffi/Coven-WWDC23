using Godot;
using System;
using CovenWWDC23Port.Data;

public partial class Bullet : Sprite2D
{
	private Area2D _hitbox;
	private SpriteSpinEnum _rotationDirection;

	private float _travelSpeed;
	private float _generalDirection;
	private float _acceleration;
	private float _curve;
	private float _directionX;
	private float _directionY;
	private int _timeToLive;

	public bool Despawn;
	private Timer _timer;
	
	// Constructor
	public Bullet(Texture2D texture, SpriteSpinEnum rotationDirection, float spawnX, float spawnY, float travelSpeed, float acceleration, float generalDirection, float curve, int timeToLive)
	{
		this.Texture = texture;
		this._rotationDirection = rotationDirection;
		
		_hitbox = new Area2D();
		_hitbox.Monitorable = true;
		AddChild(_hitbox);
		
		CollisionShape2D collisionShape = new CollisionShape2D();
		CircleShape2D circle = new CircleShape2D();
		circle.Radius = texture.GetWidth() * 0.125f;
		collisionShape.Shape = circle;
		_hitbox.AddChild(collisionShape);
		
		this._acceleration = acceleration;
		this._generalDirection = generalDirection;
		this._curve = curve;
		this._travelSpeed = travelSpeed;
		this._directionX = 0f;
		this._directionY = 0f;
		this._timeToLive = timeToLive;
		this.Despawn = false;
		
		this.Position = new Vector2(spawnX, spawnY);
		this._timer = new Timer();
		
		Callable callable = new Callable(this, "OnTimerTimeout");
		_timer.Connect("timeout", callable, 0);
		AddChild(_timer);
	}

	public void IsBulletFromPlayer(bool playerBullet)
	{
		if (playerBullet)
		{
			Name = $"pBullet({Guid.NewGuid().ToString()})";
			_hitbox.CollisionLayer = 2;
			_hitbox.CollisionMask = 4;
			this.ZIndex = 1;
		}
		else
		{
			Name = $"eBullet({Guid.NewGuid().ToString()})";
			_hitbox.CollisionLayer = 8;
			_hitbox.CollisionMask = 1;
			this.ZIndex = 3;
		}
	}

	public void updatePos()
	{
		_generalDirection = _generalDirection + _curve;
		_travelSpeed = _travelSpeed + _acceleration;

		_directionX = xDir(_generalDirection);
		_directionY = yDir(_generalDirection);

		Position = new Vector2(Position.X + _directionX * _travelSpeed, Position.Y + _directionY * _travelSpeed);

		if ((Position.X > 750) | (Position.X < -30) | (Position.Y > 990) | (Position.Y < -30)) {
			OnTimerTimeout();
		}
			
	}

	float xDir(float angle)
	{
		float radians = angle * (float)Math.PI / 180;
		return (float)Math.Cos(radians);
	}

	float yDir(float angle)
	{
		float radians = angle * (float)Math.PI / 180;
		return -(float)Math.Sin(radians);
	}
	
	public void OnTimerTimeout()
	{
		_timer.Stop();
		Despawn = true;
		this.GetParent().RemoveChild(this);
		this.QueueFree();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer.Start(_timeToLive);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (this.Texture == BulletType.Star)
			Rotate((float)delta * (int)_rotationDirection);
	}
}
