using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            xp = 0;
        }
        if (level > 1)
        {
            pauseMenuCanvas.SetActive(true);
            inventoryCanvas.SetActive(true);
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (level > 1)
        {
            SetPosition();
        }
        player.ResetHealth();
    }

    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public CharacterMenu characterMenu;
    public int xp;
    public int sceneIndex;
    public GameObject pauseMenuCanvas;
    public GameObject inventoryCanvas;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }
        if (xp >= weaponPrices[weapon.weaponLevel])
        {
            xp -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    public bool TryLevelUp()
    {
        if (xpTable.Count <= player.playerLevel)
        {
            return false;
        }
        if (xp >= xpTable[player.playerLevel])
        {
            xp -= xpTable[player.playerLevel];
            player.OnLevelUp();
            return true;
        }
        return false;
    }

    public void SaveState()
    {
        string save = $"{xp}|{player.playerLevel}|{weapon.weaponLevel}";
        PlayerPrefs.SetString("SaveState", save);
        PlayerPrefs.SetInt("SceneIndex", sceneIndex);
    }

    public void LoadState()
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        xp = int.Parse(data[0]);
        if (int.Parse(data[1]) != 1)
        {
            player.SetLevel(int.Parse(data[1]));
        }
        weapon.SetWeaponLevel(int.Parse(data[2]));
    }

    public void SetPosition()
    {
        if (PlayerPrefs.HasKey("SpawnPointIndex"))
        {
            int spawnPointIndex = PlayerPrefs.GetInt("SpawnPointIndex");
            Vector3 spawnPoint = GameObject.Find($"SpawnPoint ({spawnPointIndex})").transform.position;
            player.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, player.transform.position.z);
        }
        else
        {
            Vector3 spawnPoint = GameObject.Find("SpawnPoint").transform.position;
            player.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, player.transform.position.z);
        }
    }
}