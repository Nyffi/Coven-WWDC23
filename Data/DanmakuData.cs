using System;
using System.Text.Json.Serialization;

namespace CovenWWDC23Port.Data;

public class DanmakuData
{
    [JsonPropertyName("texture")]
    public string Texture;

    [JsonPropertyName("spriteSpin")]
    public string SpriteSpin;

    [JsonPropertyName("ownerIsPlayer")]
    public bool OwnerIsPlayer;

    [JsonPropertyName("patternArrays")]
    public int PatternArrays;

    [JsonPropertyName("bulletsPerArray")]
    public int BulletsPerArray;

    [JsonPropertyName("spreadBetweenArray")]
    public int SpreadBetweenArray;

    [JsonPropertyName("spreadWithinArray")]
    public int SpreadWithinArray;

    [JsonPropertyName("startAngle")]
    public float StartAngle;

    [JsonPropertyName("spinRate")]
    public float SpinRate;

    [JsonPropertyName("spinModificator")]
    public float SpinModificator;

    [JsonPropertyName("invertSpin")]
    public bool InvertSpin;

    [JsonPropertyName("maxSpinRate")]
    public float MaxSpinRate;

    [JsonPropertyName("fireRate")]
    public int FireRate;

    [JsonPropertyName("objectWidth")]
    public float ObjectWidth;

    [JsonPropertyName("objectHeight")]
    public float ObjectHeight;

    [JsonPropertyName("bulletSpeed")]
    public double BulletSpeed;

    [JsonPropertyName("bulletAcceleration")]
    public double BulletAcceleration;

    [JsonPropertyName("bulletCurve")]
    public double BulletCurve;

    [JsonPropertyName("bulletTTL")]
    public int BulletTTL;
}