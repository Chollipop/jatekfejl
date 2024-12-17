using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text hp, level, xp, upgradeCost, levelUpCost;
    public Image weaponSprite;
    private Animator animator;
    private bool Show = false;
    private static CharacterMenu characterMenuInstance;

    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
            GameManager.instance.SaveState();
        }
    }

    public void OnLevelUpClick()
    {
        if (GameManager.instance.TryLevelUp())
        {
            UpdateMenu();
            GameManager.instance.SaveState();
        }
    }

    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count - 1)
        {
            Button upgradeButton = GameObject.Find("Upgrade Button").GetComponent<Button>();
            upgradeButton.enabled = false;
            upgradeCost.text = "MAX";
        }
        else
        {
            upgradeCost.text = $"{GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel]} XP";
        }
        if (GameManager.instance.player.playerLevel == GameManager.instance.xpTable.Count - 1)
        {
            Button levelUpButton = GameObject.Find("Level Up Button").GetComponent<Button>();
            levelUpButton.enabled = false;
            levelUpCost.text = "MAX";
        }
        else
        {
            levelUpCost.text = $"{GameManager.instance.xpTable[GameManager.instance.player.playerLevel]} XP";
        }
        level.text = $"{(GameManager.instance.player.playerLevel + 1)}/{GameManager.instance.xpTable.Count}";
        hp.text = $"{GameManager.instance.player.hitpoint}/{GameManager.instance.player.maxHitpoint}";
        xp.text = GameManager.instance.xp.ToString();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
        if (characterMenuInstance == null)
        {
            characterMenuInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Show = !Show;
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        UpdateMenu();
        animator.SetBool("Show", Show);
    }
}