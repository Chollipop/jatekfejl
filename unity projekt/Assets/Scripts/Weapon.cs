using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public float[] pushForce = { 2.0f, 2.15f, 2.30f, 2.45f, 2.60f, 2.75f, 2.90f, 3.05f, 3.20f };
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public AudioClip swingSound;
    public AudioSource audioSource;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Swing();
        }
    }

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.CompareTag("Fighter"))
        {
            if (collider.name == "Player")
            {
                return;
            }
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            collider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");
        if (!audioSource.isPlaying && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > 1)
        {
            audioSource.PlayOneShot(swingSound);
        }
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}