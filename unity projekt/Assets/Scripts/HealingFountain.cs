using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{
    public int healingAmount = 1;
    private bool hasHealed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !hasHealed)
        {
            hasHealed = true;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
