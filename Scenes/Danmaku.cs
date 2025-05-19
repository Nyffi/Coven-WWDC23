using Godot;
using System;
using System.Collections.Generic;
using CovenWWDC23Port.Data;

namespace CovenWWDC23Port.Scenes;

public partial class Danmaku : Node2D
{
	private bool _enabled = true;
	
	// [Export(PropertyHint.Enum, BulletType)]
	// public string bulletType;
	
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
	private double _time = 0;
	private const double TimePeriod = 1;

	public void updateConfigData(BulletSpawnerConfig config)
	{
		// this._ownerIsPlayer = config.OwnerIsPlayer;
		this._texture = config.Texture;
		this._spriteSpin = config.SpriteSpin;
		// this._patternArrays = config.PatternArrays;
		// this._bulletsPerArray = config.BulletsPerArray;
		// this._spreadBetweenArray = config.SpreadBetweenArray;
		// this._spreadWithinArray = config.SpreadWithinArray;
		// this._startAngle = config.StartAngle;
		// this._spinRate = config.SpinRate;
		// this._spinModificator = config.SpinModificator;
		// this._invertSpin = config.InvertSpin;
		// this._maxSpinRate = config.MaxSpinRate;
		// this._fireRate = config.FireRate;
		// this._objectWidth = config.ObjectWidth;
		// this._objectHeight = config.ObjectHeight;
		// this._bulletSpeed = config.BulletSpeed;
		// this._bulletAcceleration = config.BulletAcceleration;
		// this._bulletCurve = config.BulletCurve;
		// this._bulletTtl = config.BulletTtl;
	}

	public BulletSpawnerConfig fetchConfigData()
	{
		BulletSpawnerConfig data = new BulletSpawnerConfig();

		data.OwnerIsPlayer = this._ownerIsPlayer;
		data.Texture = this._texture;
		data.SpriteSpin = this._spriteSpin;
		data.PatternArrays = this._patternArrays;
		data.BulletsPerArray = this._bulletsPerArray;
		data.SpreadBetweenArray = this._spreadBetweenArray;
		data.SpreadWithinArray = this._spreadWithinArray;
		data.StartAngle = this._startAngle;
		data.SpinRate = this._spinRate;
		data.SpinModificator = this._spinModificator;
		data.InvertSpin = this._invertSpin;
		data.MaxSpinRate = this._maxSpinRate;
		data.FireRate = this._fireRate;
		data.ObjectWidth = this._objectWidth;
		data.ObjectHeight = this._objectHeight;
		data.BulletSpeed = this._bulletSpeed;
		data.BulletAcceleration = this._bulletAcceleration;
		data.BulletCurve = this._bulletCurve;
		data.BulletTtl = this._bulletTtl;

		return data;
	}

	public void SelectPreMadeBulletPattern(string key)
	{
		BulletSpawnerConfig data = new BulletSpawnerConfig();
		data.LoadFromData("playerA");
		updateConfigData(data);
	}
	
	void calculation(int i, int j, float arrayAngle, float bulletAngle)
	{
		float angleCalc = _defaultAngle + (bulletAngle * i);
		angleCalc += (arrayAngle * j);
		angleCalc += _startAngle;

		float x1 = Position.X + lengthDirX(_objectWidth, angleCalc);
		float y1 = Position.Y + lengthDirY(_objectHeight, angleCalc);

		Bullet bullet = new Bullet(_texture, _spriteSpin, x1, y1, _bulletSpeed, _bulletAcceleration, angleCalc, _bulletCurve, _bulletTtl);
		bullet.IsBulletFromPlayer(_ownerIsPlayer);
		
		GetTree().Root.AddChild(bullet);
		_bulletArray.Add(bullet);
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
	
	private void ClearBulletArray(Object stateInfo) 
	{
		Callable.From(() => _bulletArray.RemoveAll(bullet => bullet.Despawn)).CallDeferred();
		GD.Print("Cleared some boolets :) " + DateTime.Now.ToString("h:mm:ss"));
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SelectPreMadeBulletPattern("playerA");
		GD.Print(this.GetParent().Name);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Position = this.GetOwner<Node2D>().Position;
		
		if (_enabled)
		{
			int bulletLength = _bulletsPerArray - 1;
			if (bulletLength == 0)
				bulletLength = 1;

			int arrayLength = _patternArrays - 1 * _patternArrays;
			if (arrayLength == 0)
				arrayLength = 1;

			int arrayAngle = _spreadWithinArray / bulletLength;
			int bulletAngle = _spreadBetweenArray / arrayLength;

			if (_shoot == 0)
			{
				for (int i = 0; i < _patternArrays; i++)
				{
					for (int j = 0; j < _bulletsPerArray; j++)
					{
						calculation(i, j, arrayAngle, bulletAngle);
					}
				}

				if (_defaultAngle > 360)
					_defaultAngle = 0;
				_defaultAngle += _spinRate;
				_spinRate += _spinModificator;

				if (_invertSpin)
					if (_spinRate < -_maxSpinRate || _spinRate > _maxSpinRate)
						_spinModificator = -_spinModificator;
			}
		}

		//bulletArray.ForEach(bullet => bullet.updatePos());
		foreach (Bullet bullet in _bulletArray)
		{
			if (!bullet.Despawn)
				bullet.updatePos();
		}

		_shoot += 1;
		if (_shoot >= _fireRate)
			_shoot = 0;

		_time += delta;

		if (_time > TimePeriod)
		{
			int lixo = _bulletArray.RemoveAll(bullet => bullet.Despawn);
			_time = 0;
			GD.Print("Array has been cleared. " + lixo + " bullets removed.");
		}
	}
}

