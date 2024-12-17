using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    public bool collected;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player" || collider.CompareTag("Player_weapon"))
        {
            OnCollect();
        }
    }
    protected virtual void OnCollect()
    {
        collected = true;
    }
}
