using UnityEngine;
using UnityEngine.AI;

public class Enemy : Mover
{
    public EnemyHealthBar enemyHealthBar;
    public int xpValue = 3;
    public float triggerLength = 10f;
    private bool isFollowing = false;
    private Transform playerTransform;
    private NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        enemyHealthBar.SetHealth(hitpoint, maxHitpoint);
    }

    protected override void Death()
    {
        GameManager.instance.player.followCount--;
        Destroy(gameObject);
        GameManager.instance.xp += xpValue;
        GameManager.instance.characterMenu.UpdateMenu();
        GameManager.instance.SaveState();
        GameManager.instance.ShowText($"+{xpValue} XP", 25, Color.white, transform.position, Vector3.up * 50, 2.0f);
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        enemyHealthBar.SetHealth(hitpoint, maxHitpoint);
    }

    private void OnEnable()
    {
        enemyHealthBar.SetHealth(hitpoint, maxHitpoint);
    }

    protected virtual void Update()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < triggerLength)
        {
            if (!isFollowing)
            {
                GameManager.instance.player.followCount++;
                isFollowing = true;
            }
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            if (isFollowing)
            {
                GameManager.instance.player.followCount--;
                isFollowing = false;
            }
            agent.ResetPath();
        }

        if (playerTransform.position.x - transform.position.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
