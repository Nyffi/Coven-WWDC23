using System;
using CovenWWDC23Port.Global;
using Godot;

namespace CovenWWDC23Port.Data;

public class BulletSpawnerConfig
{
    public Texture2D Texture;
    public SpriteSpinEnum SpriteSpin;
    public bool OwnerIsPlayer;

    // Arrays
    // bulletArray - Needs Bullet node to be made first
    public int PatternArrays;
    public int BulletsPerArray;

    // Angle Variables
    public int SpreadBetweenArray;
    public int SpreadWithinArray;
    public float StartAngle;

    // Spinning Variables
    public float SpinRate;
    public float SpinModificator;
    public bool InvertSpin;
    public float MaxSpinRate;

    // Fire Rate Variables
    public int FireRate;

    // Offsets
    public float ObjectWidth;
    public float ObjectHeight;

    // Bullet Variables
    public float BulletSpeed;
    public float BulletAcceleration;
    public float BulletCurve;
    public int BulletTtl;

    public void LoadFromData(string key)
    {
        DanmakuData data = DanmakuPatterns.Instance.FetchDanmakuData(key);

        switch (data.Texture)
        {
            case "common":
                this.Texture = BulletType.Common;
                break;
            case "dart":
                this.Texture = BulletType.Dart;
                break;
            case "star":
                this.Texture = BulletType.Star;
                break;
            case "light":
                this.Texture = BulletType.Light;
                break;
            case "heavy":
                this.Texture = BulletType.Heavy;
                break;
            default:
                this.Texture = BulletType.Common;
                break;
        }
        Enum.TryParse(data.SpriteSpin, out this.SpriteSpin);
        
        GD.Print(this.SpriteSpin);
    }
    
    
}