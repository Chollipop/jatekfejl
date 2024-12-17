using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Resolution[] resolutions;
    private List<string> options;
    private static SettingsMenu settingsMenuInstance;
    public GameObject menu;
    public GameObject pauseMenu;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Dropdown qualityDropdown;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (settingsMenuInstance == null)
        {
            settingsMenuInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        options = resolutions.Select(x => $"{x.width}x{x.height}@{x.refreshRate}").ToList();
        resolutionDropdown.AddOptions(options);
        if (PlayerPrefs.HasKey("resolutionIndex"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex");
            resolutionDropdown.RefreshShownValue();
        }
        else
        {
            resolutionDropdown.value = options.Count - 1;
            resolutionDropdown.RefreshShownValue();
        }
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            bool isFullscreen = PlayerPrefs.GetInt("isFullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;
        }
        if (PlayerPrefs.HasKey("qualityIndex"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("qualityIndex");
            qualityDropdown.RefreshShownValue();
        }
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);
        Application.targetFrameRate = resolution.refreshRate;
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("isFullscreen", Convert.ToInt32(isFullscreen));
        resolutionDropdown.value = options.Count - 1;
        resolutionDropdown.RefreshShownValue();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            menu = GameObject.Find("MainCanvas").transform.GetChild(1).gameObject;
        }
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
    }

    public void Back()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ShowMenu();
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }
}
