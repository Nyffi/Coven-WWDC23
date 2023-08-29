using Godot;
using System.Collections.Generic;
using System;

interface Enemy
{
    BulletSpawner mainSpawner { get; set; }
    //BulletSpawner? subSpawner { get; set; }
    List<Action> actionPhases { get; set; }
    int health { get; set; }
    int maxHealth { get; set; }
    bool canTakeDamage { get; set; }
    bool canShoot { get; set; }
    bool isActive { get; set; }
    CollisionShape2D hitbox { get; set; }
    AnimatedSprite2D sprite { get; set; }

    void updateSpawner(BulletSpawnerConfig main);
    void setupActionPhase(Action action);
    void executePhase(bool progressThroughList);
}