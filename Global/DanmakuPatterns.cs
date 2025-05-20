using System;
using System.Collections.Generic;
using Godot;
using System.Text.Json;
using System.Text.Json.Serialization;
using CovenWWDC23Port.Data;
using CovenWWDC23Port.Utils;

namespace CovenWWDC23Port.Global;

public partial class DanmakuPatterns : Node
{
	private Json _patterns = ResourceLoader.Load<Json>("res://Data/Patterns.json");
	private Dictionary<string, DanmakuData> _patternData;
	//private static DanmakuPatterns _instance;

	// public static DanmakuPatterns Instance
	// {
	// 	get
	// 	{
	// 		if (_instance == null)
	// 			GD.Print(Engine.GetSingletonList());
	// 			_instance = (DanmakuPatterns)Engine.GetSingleton("DanmakuPatterns");
	// 		return _instance;
	// 	}
	// }
	
	public static DanmakuPatterns Instance { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		
		GD.Print("Loading DanmakuPatterns...");
		Dictionary<string, DanmakuData> data = new Dictionary<string, DanmakuData>();
		try
		{
			data = JsonSerializer.Deserialize<Dictionary<string, DanmakuData>>(
				_patterns.Data.ToString(),
				new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, IncludeFields = true, Converters = { new FlexibleIntConverter() }});
		}
		catch (Exception e)
		{
			GD.Print(e.Message);
		}
		
		_patternData = data;
		GD.Print("DanmakuPatterns loaded");
	}

	// Fetch a specific key entry from the data dictionary
	public DanmakuData FetchDanmakuData(string key)
	{
		return _patternData[key];
	}
}
