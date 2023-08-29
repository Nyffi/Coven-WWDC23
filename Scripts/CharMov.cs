using Godot;
using System;
using System.Collections.Generic;

public partial class CharMov : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public List<Sprite2D> bullets = new List<Sprite2D>();

	BulletSpawner mainSpawner = new BulletSpawner();
	BulletSpawner flankSpawner = new BulletSpawner();

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Vector2 mousePos = GetViewport().GetMousePosition();

		//Velocity = inputDirection * Speed;
		if (mousePos.X < 20)
			mousePos.X = 20;
		else if (mousePos.X > 700)
			mousePos.X = 700;

		if (mousePos.Y < 20)
			mousePos.Y = 20;
		else if (mousePos.Y > 940)
			mousePos.Y = 940;

		Position = mousePos;
		mainSpawner.Position = Position;
		flankSpawner.Position = Position;
		//GD.Print("Spawner pos: ", mainSpawner.Position);
	}

	public override void _Input(InputEvent @event)
	{
		// Mouse in viewport coordinates.
		if (@event is InputEventMouseButton eventMouseButton)
			//GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
			if (eventMouseButton.Pressed)
			{
				DebugSpawnBulletTest(eventMouseButton.Position);
			}
		else if (@event is InputEventMouseMotion eventMouseMotion)
			GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

		// Print the size of the viewport.
		//GD.Print("Viewport Resolution is: ", GetViewportRect().Size);
	}

	public void DebugSpawnBulletTest(Vector2 pos)
	{
		Texture2D texture = BulletType.star;
		Bullet bullet = new Bullet(texture, SpriteSpinEnum.Clockwise, 0, 0, 0, 0, 0, 0, 5);

		bullet.Position = pos;
		
		GetTree().Root.AddChild(bullet);
		bullets.Add(bullet);

		//Fairy fairy = new Fairy(FairyClass.Light);
		//fairy.Position = new Vector2(400, 400);
		//GetTree().Root.AddChild(fairy);

		//Fairy fairy2 = new Fairy(FairyClass.Heavy);
		//fairy2.Position = new Vector2(200, 400);
		//GetTree().Root.AddChild(fairy2);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			//velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		GetInput();
		MoveAndSlide();
	}

	public override void _Ready()
	{
		//Callable callable = new Callable(this, "OnTimerTimeout");
		//time.Connect("timeout", callable, 0);
		//AddChild(time);

		mainSpawner.updateConfigData(SpawnerPresets.GetInstance().playerA);
		AddChild(mainSpawner);

		flankSpawner.updateConfigData(SpawnerPresets.GetInstance().playerB);
		AddChild(flankSpawner);
	}
}

