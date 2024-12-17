using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FogWall : MonoBehaviour
{
    public GameObject boss;
    public AudioClip audioClip;
    public PolygonCollider2D polygonCollider;
    private AudioSource audioSource;
    private bool hasEntered = false;

    private void Start()
    {
        audioSource = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boss != null)
        {
            if (PlayerPrefs.GetInt(boss.name) != 1)
            {
                if (!collision.CompareTag("Player_weapon") && collision.name == "Player")
                {
                    if (!hasEntered)
                    {
                        hasEntered = true;
                        boss.SetActive(true);
                        polygonCollider.enabled = true;
                        audioSource.clip = audioClip;
                        audioSource.Play();
                    }
                }
            }
        }
    }
}
