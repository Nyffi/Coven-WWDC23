using Godot;

namespace CovenWWDC23Port.Scenes;

public partial class Player : Node2D
{
	[Export] private Danmaku _main;
	[Export] private Danmaku _second;
	
	private bool _invulnerable; 
	private void UpdatePlayerPosition()
	{
		Vector2 mousePos = GetViewport().GetMousePosition();
		
		if (mousePos.X < 20)
			mousePos.X = 20;
		else if (mousePos.X > 700)
			mousePos.X = 700;

		if (mousePos.Y < 25)
			mousePos.Y = 25;
		else if (mousePos.Y > 935)
			mousePos.Y = 935;
		
		//GD.Print($"Pos: {mousePos}");
		Position = mousePos;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// GetNode<CanvasLayer>("player_layer");
		this.ZIndex = 2;
		_main.SelectPreMadeBulletPattern("playerA");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdatePlayerPosition();
	}

	private void OnDamage(Area2D area)
	{
		GD.Print("Player took damage");
	}
	
	private void OnGraze(Area2D area)
	{
		GD.Print("Player grazed a projectile");
	}
}