using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint;
    public int maxHitpoint;
    public float lastImmune;
    public float pushRecoverySpeed = 0.2f;
    protected Vector3 pushDirection;
    protected float immuneTime = 1.0f;

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = new Vector3((transform.position.x - dmg.origin.x) * dmg.pushForce, (transform.position.y - dmg.origin.y) * dmg.pushForce, transform.position.z);
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
            if (name == "Player")
            {
                GameManager.instance.characterMenu.UpdateMenu();
                GameManager.instance.player.SetHealth();
                if (!GameManager.instance.player.audioSource.isPlaying)
                {
                    GameManager.instance.player.audioSource.PlayOneShot(GameManager.instance.player.hurtSound);
                }
            }
        }
    }

    protected virtual void Death()
    { }
}


