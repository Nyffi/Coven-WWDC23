using Godot;
using System;
using System.Collections.Generic;

// Maybe probably turn this into a singleton	[DONE]

public partial class EnemyManager : Node2D
{
	private List<Fairy> fairies = new List<Fairy>();
	//TODO: Put Boss here
	private Fairy test = new Fairy(fairyClass: FairyClass.Light, SpawnerPresets.GetInstance().hell);

	private EnemyManager() 
	{ 
		int auxX = 30;
		int auxY = 30;
		BulletSpawnerConfig lightPreset = SpawnerPresets.GetInstance().lightFairy;
		BulletSpawnerConfig heavyPreset = SpawnerPresets.GetInstance().heavyFairy;

		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				Fairy light = new Fairy(FairyClass.Light, lightPreset);
				light.Position = new Vector2(auxX - 15, auxY);
				light.Name = "enemy_fairyLight_" + i + "_" + j;
				light.ZIndex = -1;
				// lmao code
				this.fairies.Add(light);
				AddChild(light);

				Fairy heavy = new Fairy(FairyClass.Heavy, heavyPreset);
				heavy.Position = new Vector2(auxX + 15, auxY);
				heavy.Name = "enemy_fairyHeavy_" + i + "_" + j;
				heavy.ZIndex = -1;
				// lmao more code
				this.fairies.Add(heavy);
				AddChild(heavy);

				auxX += 80;
				
			}
			auxX = 30;
			auxY += 120;
		}
		test.Position = new Vector2(350, 40);
		//AddChild(test);
		// TODO: Boss setup & logic
	}
	private static EnemyManager _instance;
	public static EnemyManager GetInstance()
	{
		if (_instance == null)
			_instance = new EnemyManager();
		return _instance;
	}

	private void SpawnCommonEnemies(int max)
	{
		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetInstance();	// Source: trust me 👍
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
