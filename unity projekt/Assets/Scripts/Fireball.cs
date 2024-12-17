using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage;
    private float timer;

    void Start()
    { }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }

    public void Shoot(Transform fireFrom, int rotate = 0, float spreadAngle = 0f)
    {
        Transform playerTransform = GameManager.instance.player.transform;
        Vector3 direction = playerTransform.position - fireFrom.position;
        Quaternion rotation = Quaternion.AngleAxis(rotate * spreadAngle, Vector3.forward);
        Vector2 angle = rotation * new Vector2(direction.x, direction.y).normalized;
        Fireball fireball = Instantiate(this, fireFrom.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().velocity = angle * 7.5f;
    }

    public void ShootInACircle(Transform fireFrom, int numProjectiles = 16, float speed = 30f)
    {
        float angleStep = 360f / numProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numProjectiles - 1; i++)
        {
            float projectileDirXposition = fireFrom.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 1f;
            float projectileDirYposition = fireFrom.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 1f;

            Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
            Vector3 projectileMoveDirection = (projectileVector - fireFrom.position).normalized * speed;

            Fireball fireball = Instantiate(this, fireFrom.position, Quaternion.identity);
            fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = 0
            };
            collider.SendMessage("ReceiveDamage", dmg);
        }
    }
}
