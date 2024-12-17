using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource audioSource;
    public AudioClip audioClip;
    private static PauseMenu pauseMenuInstance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        GameManager.instance.SaveState();
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        GameObject SpawnPoint = new GameObject();
        if (PlayerPrefs.HasKey("SpawnPointIndex"))
        {
            SpawnPoint.name = $"SpawnPoint ({PlayerPrefs.GetInt("SpawnPointIndex")})";
        }
        else
        {
            SpawnPoint.name = "SpawnPoint";
        }
        DontDestroyOnLoad(SpawnPoint);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void QuitGame()
    {
        GameManager.instance.SaveState();
        Application.Quit();
    }
}
