using Godot;
using System;

public partial class Bullet : Sprite2D
{
	// TODO: Deixar a associacao de collision layer e collision mask associada diretamente no metodo construtor em vez de deixar o Area2D publico
	public Area2D hitboxArea;
	CollisionShape2D hitbox;
	SpriteSpinEnum rotationDir;

	float travelSpeed;
	float direction;
	float acceleration;
	float curve;
	float dirX;
	float dirY;
	int ttl;

	public bool despawn;
	Timer timer;

	public Bullet(Texture2D sprite, SpriteSpinEnum rotationDir, float spawnX, float spawnY, float travelSpeed, float acceleration, float direction, float curve, int ttl)
	{
		this.Texture = sprite;
		this.rotationDir = rotationDir;

		hitboxArea = new Area2D();
		hitboxArea.Monitorable = true;
		AddChild(hitboxArea);

		hitbox = new CollisionShape2D();
		CircleShape2D shape = new CircleShape2D();
		shape.Radius = sprite.GetWidth() * 0.2f;
		hitbox.Shape = shape;
		hitboxArea.AddChild(hitbox);

		this.acceleration = acceleration;
		this.direction = direction;
		this.curve = curve;
		this.travelSpeed = travelSpeed;
		this.dirX = 0.0f;
		this.dirY = 0.0f;
		this.ttl = ttl;
		this.despawn = false;

		this.Position = new Vector2(spawnX, spawnY);
		this.timer = new Timer();

		Callable callable = new Callable(this, "OnTimerTimeout");
		timer.Connect("timeout", callable, 0);
        AddChild(timer);
	}

	public void updatePos()
	{
		direction = direction + curve;
		travelSpeed = travelSpeed + acceleration;

		dirX = xDir(direction);
		dirY = yDir(direction);

		Position = new Vector2(Position.X + dirX * travelSpeed, Position.Y + dirY * travelSpeed);

		despawn = (Position.X > 750) || (Position.X < -30) || (Position.Y > 990) || (Position.Y < -30);
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
		despawn = true;
		QueueFree();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer.Start(this.ttl);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (this.Texture == BulletType.star)
			Rotate((float)delta * (int)rotationDir);
	}
}
