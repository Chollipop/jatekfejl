using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : EnemyHealthBar
{
    public override void SetHealth(int health, int maxHealth)
    {
        healthBar.value = health;
        healthBar.maxValue = maxHealth;
    }

    public override void Update()
    { }
}
