using System;

public class SpawnerPresets
{
    private static SpawnerPresets _instance;
    public BulletSpawnerConfig playerA;
    public BulletSpawnerConfig playerB;
    public BulletSpawnerConfig lightFairy;
    public BulletSpawnerConfig heavyFairy;
    public BulletSpawnerConfig boss;

    // Refactor suggestion: Put all data on a JSON file and read it from there instead.
    private SpawnerPresets()
    {
        this.playerA = new BulletSpawnerConfig();
		playerA.texture = BulletType.dart;
		playerA.spriteSpin = SpriteSpinEnum.None;
		playerA.ownerIsPlayer = true;
		playerA.patternArrays = 1;
		playerA.bulletsPerArray = 3;
		playerA.spreadBetweenArray = 0;
		playerA.spreadWithinArray = 5;
		playerA.startAngle = 88;
		playerA.spinRate = 0;
		playerA.spinModificator = 0;
		playerA.invertSpin = true;
		playerA.maxSpinRate = 1;
		playerA.fireRate = 6;
		playerA.objectWidth = 25;
		playerA.objectHeight = 1;
		playerA.bulletSpeed = 12.5f;
		playerA.bulletAcceleration = 0;
		playerA.bulletCurve = 0;
		playerA.bulletTTL = 5;

        this.playerB = new BulletSpawnerConfig();
		playerB.texture = BulletType.star;
		playerB.spriteSpin = SpriteSpinEnum.Clockwise;
		playerB.ownerIsPlayer = true;
		playerB.patternArrays = 1;
		playerB.bulletsPerArray = 2;
		playerB.spreadBetweenArray = 0;
		playerB.spreadWithinArray = 12;
		playerB.startAngle = 84;
		playerB.spinRate = 0;
		playerB.spinModificator = 0;
		playerB.invertSpin = true;
		playerB.maxSpinRate = 1;
		playerB.fireRate = 6;
		playerB.objectWidth = 100;
		playerB.objectHeight = 1;
		playerB.bulletSpeed = 12.5f;
		playerB.bulletAcceleration = 0;
		playerB.bulletCurve = 0;
		playerB.bulletTTL = 5;

        this.lightFairy = new BulletSpawnerConfig();
		lightFairy.texture = BulletType.light;
		lightFairy.spriteSpin = SpriteSpinEnum.None;
		lightFairy.ownerIsPlayer = false;
		lightFairy.patternArrays = 1;
		lightFairy.bulletsPerArray = 3;
		lightFairy.spreadBetweenArray = 1;
		lightFairy.spreadWithinArray = 20;
		lightFairy.startAngle = 260;
		lightFairy.spinRate = 0;
		lightFairy.spinModificator = 1;
		lightFairy.invertSpin = true;
		lightFairy.maxSpinRate = 1;
		lightFairy.fireRate = 50;
		lightFairy.objectWidth = 25;
		lightFairy.objectHeight = 1;
		lightFairy.bulletSpeed = 3f;
		lightFairy.bulletAcceleration = 0;
		lightFairy.bulletCurve = 0;
		lightFairy.bulletTTL = 10;

        this.heavyFairy = new BulletSpawnerConfig();
		heavyFairy.texture = BulletType.heavy;
		heavyFairy.spriteSpin = SpriteSpinEnum.None;
		heavyFairy.ownerIsPlayer = false;
		heavyFairy.patternArrays = 1;
		heavyFairy.bulletsPerArray = 3;
		heavyFairy.spreadBetweenArray = 1;
		heavyFairy.spreadWithinArray = 50;
		heavyFairy.startAngle = 242;
		heavyFairy.spinRate = 0;
		heavyFairy.spinModificator = 1;
		heavyFairy.invertSpin = true;
		heavyFairy.maxSpinRate = 1;
		heavyFairy.fireRate = 150;
		heavyFairy.objectWidth = 25;
		heavyFairy.objectHeight = 1;
		heavyFairy.bulletSpeed = 0.25f;
		heavyFairy.bulletAcceleration = 0.005f;
		heavyFairy.bulletCurve = 0;
		heavyFairy.bulletTTL = 15;

        this.boss = new BulletSpawnerConfig();
		boss.texture = BulletType.heavy;
		boss.spriteSpin = SpriteSpinEnum.None;
		boss.ownerIsPlayer = false;
		boss.patternArrays = 1;
		boss.bulletsPerArray = 3;
		boss.spreadBetweenArray = 1;
		boss.spreadWithinArray = 50;
		boss.startAngle = 242;
		boss.spinRate = 0;
		boss.spinModificator = 1;
		boss.invertSpin = true;
		boss.maxSpinRate = 1;
		boss.fireRate = 150;
		boss.objectWidth = 25;
		boss.objectHeight = 1;
		boss.bulletSpeed = 0.25f;
		boss.bulletAcceleration = 0.005f;
		boss.bulletCurve = 0;
		boss.bulletTTL = 15;
    }

    public static SpawnerPresets GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SpawnerPresets();
        }

        return _instance;
    }
}