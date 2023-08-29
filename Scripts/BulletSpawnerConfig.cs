using Godot;
using System;

public class BulletSpawnerConfig
{
    public Texture2D texture;
	public SpriteSpinEnum spriteSpin;
	public bool ownerIsPlayer;

	// Arrays
	// bulletArray - Needs Bullet node to be made first
	public int patternArrays;
	public int bulletsPerArray;

	// Angle Variables
	public int spreadBetweenArray;
	public int spreadWithinArray;
	public float startAngle;

	// Spinning Variables
	public float spinRate;
	public float spinModificator;
	public bool invertSpin;
	public float maxSpinRate;

	// Fire Rate Variables
	public int fireRate;

	// Offsets
	public float objectWidth;
	public float objectHeight;

	// Bullet Variables
	public float bulletSpeed;
	public float bulletAcceleration;
	public float bulletCurve;
	public int bulletTTL;
}

public struct SpawnerConfigs
{
	public Texture2D texture;
	public SpriteSpinEnum spriteSpin;
	public bool ownerIsPlayer;

	// Arrays
	public int patternArrays;
	public int bulletsPerArray;

	// Angle Variables
	public int spreadBetweenArray;
	public int spreadWithinArray;
	public float startAngle;

	// Spinning Variables
	public float spinRate;
	public float spinModificator;
	public bool invertSpin;
	public float maxSpinRate;

	// Fire Rate Variables
	public int fireRate;

	// Offsets
	public float objectWidth;
	public float objectHeight;

	// Bullet Variables
	public float bulletSpeed;
	public float bulletAcceleration;
	public float bulletCurve;
	public int bulletTTL;
}

public enum SpriteSpinEnum
{
    None = 0,
    Clockwise = -345,
    CounterClockwise = 345
}