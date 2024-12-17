using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public void ResetMusic()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
