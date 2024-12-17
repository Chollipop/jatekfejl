using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && GameManager.instance.player.followCount == 0)
        {
            GameManager.instance.player.ResetHealth();
            if (name.Contains("("))
            {
                int spawnPointIndex = int.Parse(name.Split('(')[1].Split(')')[0]);
                PlayerPrefs.SetInt("SpawnPointIndex", spawnPointIndex);
            }
            GameManager.instance.SaveState();
        }
    }
}
