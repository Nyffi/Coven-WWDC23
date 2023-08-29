using Godot;
using System;
using System.Collections.Generic;

public partial class Fairy : Node2D, Enemy
{	
	public BulletSpawner mainSpawner { get; set; }
    //BulletSpawner? subSpawner { get; set; }
    public List<Action> actionPhases { get; set; }
    public int health { get; set; }
    public int maxHealth { get; set; }
    public bool canTakeDamage { get; set; }
    public bool canShoot { get; set; }
    public bool isActive { get; set; }
    public CollisionShape2D hitbox { get; set; }
    public AnimatedSprite2D sprite { get; set; }
    
	public Fairy(FairyClass fairyClass, BulletSpawnerConfig config)
	{
		this.sprite = new AnimatedSprite2D();
		this.sprite.SpriteFrames = GD.Load("res://Assets/Enemy/enemyAnim.tres") as SpriteFrames;
		this.hitbox = new CollisionShape2D();
		RectangleShape2D rect = new RectangleShape2D();

		switch (fairyClass)
		{
			case FairyClass.Light:
				this.sprite.Animation = "fairyLight";
				rect.Size = this.sprite.SpriteFrames.GetFrameTexture("fairyLight", 0).GetSize();
				this.maxHealth = this.health = 20;
				break;
			case FairyClass.Heavy:
				this.sprite.Animation = "fairyHeavy";
				rect.Size = this.sprite.SpriteFrames.GetFrameTexture("fairyHeavy", 0).GetSize();
				this.maxHealth = this.health = 60;
				break;
		}

		rect.Size *= new Vector2(0.65f, 0.8f);
		this.hitbox.Shape = rect;
		this.sprite.Play();
		AddChild(sprite);
		AddChild(hitbox);

		this.mainSpawner = new BulletSpawner();
		this.mainSpawner.updateConfigData(config);

		this.isActive = false;
		this.canTakeDamage = true;
		this.canShoot = false;
	}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void updateSpawner(BulletSpawnerConfig main)
    {
		this.mainSpawner.updateConfigData(main);
    }

    public void setupActionPhase(Action action)
    {
        throw new NotImplementedException();
    }

    public void executePhase(bool progressThroughList)
    {
        throw new NotImplementedException();
    }
}

public enum FairyClass
{
	Light,
	Heavy
}