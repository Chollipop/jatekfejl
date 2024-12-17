using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    public Fireball fireball;

    private float timer;

    void Start()
    { }

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position);
        
        if (distance < 10)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                ShootFireball();
            }
        }
    }

    private void ShootFireball()
    {
        fireball.Shoot(this.GetComponentInChildren<Transform>());
    }
}
