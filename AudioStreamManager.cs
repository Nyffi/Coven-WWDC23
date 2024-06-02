using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class AudioStreamManager : Node
{
	int numPlayers = 10; 
	String bus = "master";

	Queue<AudioStreamPlayer> available = new Queue<AudioStreamPlayer>();
	List<AudioStreamPlayer> occupied = new List<AudioStreamPlayer>();
	Queue<String> queue = new Queue<string>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < numPlayers; i++) 
		{
			AudioStreamPlayer audio = new AudioStreamPlayer();
			AddChild(audio);
			available.Enqueue(audio);
			audio.Connect("finished", new Callable(this, "OnStreamFinished"));
			audio.Bus = this.bus;
		}
	}

	public void OnStreamFinished(AudioStreamPlayer stream)
	{
		this.available.Enqueue(stream);
		this.occupied.Remove(stream);
	}

	public void Play(string soundPath, bool loop)
	{
		this.queue.Enqueue(soundPath);
		GD.Print("audio enqueued");
	}

	public void DequeueAll()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (queue.Any() && available.Any())
		{
			AudioStreamPlayer player = available.Dequeue();
			player.Stream = GD.Load(queue.Dequeue()) as AudioStreamMP3;
			player.Play();
			
			occupied.Add(player);
		}
	}
}

struct SoundEffects
{
	public const string shoot = "res://Assets/Sound/SFX/shoot.mp3";
	public const string hit = "res://Assets/Sound/SFX/hit.mp3";
	public const string graze = "res://Assets/Sound/SFX/graze.mp3";
	public const string gotHit = "res://Assets/Sound/SFX/gotHit.mp3";
}

struct Music
{
	public const string menuMusic = "res://Assets/Sound/Music/menuMusic.mp3";
	public const string levelMusic = "res://Assets/Sound/Music/levelMusic.mp3";
	public const string bossMusic = "res://Assets/Sound/Music/bossMusic.mp3";
}