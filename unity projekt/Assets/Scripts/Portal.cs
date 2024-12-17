using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public List<string> scenes = new List<string>();
    public int currentSceneIndex = 0;

    public new void Start()
    {
        base.Start();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes.Add(SceneManager.GetSceneAt(i).path);
        }
    }

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            GameManager.instance.SaveState();
            currentSceneIndex++;
            SceneManager.LoadScene(scenes[currentSceneIndex]);
        }
    }
}
