using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Mover
{
    public Slider hpBar;
    public GameObject deathMenu;
    public AudioSource audioSource;
    public AudioClip hurtSound;
    public float moveSpeed = 8f;
    public int playerLevel = 0;
    public int followCount = 0;
    private static Player playerInstance;

    public void OnLevelUp()
    {
        playerLevel++;
        maxHitpoint += 5;
        hitpoint += 5;
        hpBar.maxValue += 5;
        SetHealth();
    }

    public void SetLevel(int level)
    {
        playerLevel = level;
        maxHitpoint = (level + 2) * 5;
        hitpoint = (level + 2) * 5;
        hpBar.maxValue = (level + 2) * 5;
        SetHealth();
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
        {
            return;
        }
        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        }
        GameManager.instance.ShowText($"+{healingAmount} HP", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.characterMenu.UpdateMenu();
        SetHealth();
    }

    public void SetHealth()
    {
        hpBar.value = hitpoint;
    }

    public void ResetHealth()
    {
        hitpoint = maxHitpoint;
        hpBar.value = maxHitpoint;
    }

    public void Respawn()
    {
        deathMenu.SetActive(false);
        gameObject.SetActive(true);
        GameManager.instance.pauseMenuCanvas.SetActive(true);
        GameManager.instance.inventoryCanvas.SetActive(true);
        SceneManager.LoadScene(GameManager.instance.sceneIndex);
        ResetHealth();
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);
        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GameManager.instance.LoadState();
    }

    protected override void Death()
    {
        gameObject.SetActive(false);
        deathMenu.SetActive(true);
        GameManager.instance.pauseMenuCanvas.SetActive(false);
        GameManager.instance.inventoryCanvas.SetActive(false);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        UpdateMotor(new Vector3(x, y, 0).normalized, moveSpeed);
    }
}