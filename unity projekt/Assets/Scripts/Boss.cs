using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    public Fireball fireball;
    public FogWall fogWall;
    private float timer;

    protected override void Start()
    {
        base.Start();
        if (PlayerPrefs.GetInt(this.name) != 1)
        {
            audioSource = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
            fogWall.polygonCollider.enabled = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            ShootFireball();
        }
    }

    protected override void Death()
    {
        base.Death();
        audioSource.clip = audioClip;
        audioSource.Play();
        fogWall.polygonCollider.enabled = false;
        PlayerPrefs.SetInt(this.name, 1);
    }

    private void ShootFireball()
    {
        Transform fireFrom = System.Array.Find(this.GetComponentsInChildren<Transform>(), x => x.name == "FireFrom").transform;
        fireball.Shoot(fireFrom, -1, 25f);
        fireball.Shoot(fireFrom);
        fireball.Shoot(fireFrom, 1, 25f);
    }
}
