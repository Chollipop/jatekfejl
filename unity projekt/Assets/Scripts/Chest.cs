using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int xpAmount;

    public new void Start()
    {
        base.Start();
        if (PlayerPrefs.GetInt(this.name) != 1)
        {
            xpAmount = Random.Range(5, 11);
        }
        else
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
        }
    }

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.xp += xpAmount;
            GameManager.instance.characterMenu.UpdateMenu();
            GameManager.instance.SaveState();
            GameManager.instance.ShowText($"+{xpAmount} XP", 25, Color.white, transform.position, Vector3.up * 50, 2.0f);
            PlayerPrefs.SetInt(this.name, 1);
        }
    }
}
