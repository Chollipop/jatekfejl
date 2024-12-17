using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    public VideoPlayer videoplayer;

    private void Awake()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void LoadScene(VideoPlayer vp)
    {
    }
}