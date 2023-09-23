using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class BulletSpawner : Node2D
{
	Texture2D texture;
	SpriteSpinEnum spriteSpin = SpriteSpinEnum.None;
	bool ownerIsPlayer;

	// Arrays
	List<Bullet> bulletArray = new List<Bullet>();
	int patternArrays = 1;
	int bulletsPerArray = 1;

	// Angle Variables
	int spreadBetweenArray = 300;
	int spreadWithinArray = 90;
	float startAngle = 0.0f;
	float defaultAngle = 0.0f;

	// Spinning Variables
	float spinRate = 0.0f;
	float spinModificator = 0.0f;
	bool invertSpin = true;
	float maxSpinRate = 10.0f;

	// Fire Rate Variables
	int fireRate = 5;
	int shoot = 0;

	// Offsets
	float objectWidth = 0.0f;
	float objectHeight = 0.0f;
	float xOffset = 0.0f;
	float yOffset = 0.0f;

	// Bullet Variables
	float bulletSpeed = 3.0f;
	float bulletAcceleration = 0.0f;
	float bulletCurve = 0.0f;
	int bulletTTL = 10;

	// Clear Timer
	System.Threading.Timer timer;
	double time = 0;
	const double timePeriod = 1;

	public void updateConfigData(BulletSpawnerConfig config)
	{
		this.ownerIsPlayer = config.ownerIsPlayer;
		this.texture = config.texture;
		this.spriteSpin = config.spriteSpin;
		this.patternArrays = config.patternArrays;
		this.bulletsPerArray = config.bulletsPerArray;
		this.spreadBetweenArray = config.spreadBetweenArray;
		this.spreadWithinArray = config.spreadWithinArray;
		this.startAngle = config.startAngle;
		this.spinRate = config.spinRate;
		this.spinModificator = config.spinModificator;
		this.invertSpin = config.invertSpin;
		this.maxSpinRate = config.maxSpinRate;
		this.fireRate = config.fireRate;
		this.objectWidth = config.objectWidth;
		this.objectHeight = config.objectHeight;
		this.bulletSpeed = config.bulletSpeed;
		this.bulletAcceleration = config.bulletAcceleration;
		this.bulletCurve = config.bulletCurve;
		this.bulletTTL = config.bulletTTL;
	}

	void calculation(int i, int j, float arrayAngle, float bulletAngle)
	{
		float angleCalc = defaultAngle + (bulletAngle * i);
		angleCalc += (arrayAngle * j);
		angleCalc += startAngle;

		float x1 = Position.X + lengthDirX(objectWidth, angleCalc);	// Original -> xOffset
		float y1 = Position.Y + lengthDirY(objectHeight, angleCalc);	// Original -> yOffset

		Bullet bullet = new Bullet(this.texture, this.spriteSpin, x1, y1, bulletSpeed, bulletAcceleration, angleCalc, bulletCurve, bulletTTL);
		// Make the boolet speeeen
		if (ownerIsPlayer)
		{
			bullet.Name = "pBullet(" + Guid.NewGuid().ToString() + ")";
			// set the boolet bitmasks (or equivalent of)
		}
		else
		{
			bullet.Name = "eBullet(" + Guid.NewGuid().ToString() + ")";
			// set the boolet bitmasks (or equivalent of)
		}

		GetTree().Root.AddChild(bullet);
		bulletArray.Add(bullet);
	}

	// Trigonometry functions
	float lengthDirX(float dist, float angle)
	{
		return dist * (float)Math.Cos((angle * (float)Math.PI) / 180);
	}

	float lengthDirY(float dist, float angle)
	{
		return dist * -(float)Math.Sin((angle * (float)Math.PI) / 180);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//TimerCallback callback = new TimerCallback(ClearBulletArray);
		//this.timer = new System.Threading.Timer(callback, null, 0, 500);
	}

	private void ClearBulletArray(Object stateInfo) 
	{
		//List<Bullet> despawnArray = this.bulletArray.FindAll(bullet => bullet.despawn);
		//despawnArray.ForEach(b => b.QueueFree());
		Callable.From(() => bulletArray.RemoveAll(bullet => bullet.despawn)).CallDeferred();
		//GD.Print("Cleared some boolets :) " + DateTime.Now.ToString("h:mm:ss"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int bulletLength = bulletsPerArray - 1;
		if (bulletLength == 0)
			bulletLength = 1;

		int arrayLength = patternArrays - 1 * patternArrays;
		if (arrayLength == 0)
			arrayLength = 1;

		int arrayAngle = spreadWithinArray / bulletLength;
		int bulletAngle = spreadBetweenArray / arrayLength;

		if (shoot == 0)
		{
			for(int i = 0; i < patternArrays; i++)
			{
				for(int j = 0; j < bulletsPerArray; j++)
				{
					calculation(i, j, arrayAngle, bulletAngle);
				}
			}

			if (defaultAngle > 360)
				defaultAngle = 0;
			defaultAngle += spinRate;
			spinRate += spinModificator;

			if (invertSpin)
				if (spinRate < -maxSpinRate || spinRate > maxSpinRate)
					spinModificator = -spinModificator;
		}

		//bulletArray.ForEach(bullet => bullet.updatePos());
		foreach (Bullet bullet in bulletArray)
		{
			if (!bullet.despawn)
				bullet.updatePos();
		}

		shoot += 1;
		if (shoot >= fireRate)
			shoot = 0;

		time += delta;

		if (time > timePeriod)
		{
			int lixo = bulletArray.RemoveAll(bullet => bullet.despawn);
			time = 0;
			GD.Print("Array has been cleared. " + lixo + " bullets removed.");
		}
	}
}
