using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas inventoryCanvas;
    public Canvas pauseMenuCanvas;
    public Player player;
    public GameObject settingsCanvas;

    private void Start()
    {
        inventoryCanvas.gameObject.SetActive(false);
        pauseMenuCanvas.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        inventoryCanvas.gameObject.SetActive(true);
        pauseMenuCanvas.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("SceneIndex"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SceneIndex"));
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        if (PlayerPrefs.HasKey("SpawnPointIndex"))
        {
            Destroy(GameObject.Find($"SpawnPoint ({PlayerPrefs.GetInt("SpawnPointIndex")})"));
        }
        else
        {
            Destroy(GameObject.Find("SpawnPoint"));
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            inventoryCanvas.gameObject.SetActive(false);
            pauseMenuCanvas.gameObject.SetActive(false);
            settingsCanvas = GameObject.Find("OptionsCanvas").transform.GetChild(1).gameObject;
        }
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
    }
}
