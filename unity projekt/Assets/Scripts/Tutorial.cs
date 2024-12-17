using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text messageText;
    public GameObject portal;
    public Chest chest;
    private bool w = false, a = false, s = false, d = false, hasMoved = false, hasAttacked = false, hasOpenedMenu = false, hasLootedChest = false;

    private void Start()
    {
        portal.SetActive(false);
        chest.enabled = false;
        messageText.text = "Welcome to Knight of Vengeance! This tutorial section will walk you through the basics of the game. Press the 'WASD' keys to move!";
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            w = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            a = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            s = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            d = true;
        }
        if (w && a && s && d && !hasMoved)
        {
            hasMoved = true;
            AttackPrompt();
        }
        if (hasMoved && !hasAttacked)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                hasAttacked = true;
                InventoryPrompt();
            }
        }
        if (hasAttacked && !hasOpenedMenu)
        {
            if (Input.GetKey(KeyCode.I))
            {
                hasOpenedMenu = true;
                ChestPrompt();
            }
        }
        if (hasOpenedMenu && !hasLootedChest)
        {
            if (chest.collected)
            {
                hasLootedChest = true;
                FinalPrompt();
            }
        }
    }

    private void AttackPrompt()
    {
        messageText.text = "Great! Now try attacking by pressing the 'Space' key.";
    }

    private void InventoryPrompt()
    {
        messageText.text = "Great! Now open your character's upgrade menu by pressing 'I'. Here, you can upgrade your character and weapon.";
    }

    private void ChestPrompt()
    {
        chest.enabled = true;
        messageText.text = "Excellent! Now you know the basic controls of the game. You can also loot XP from chests and heal by going near healing fountains. You can find these objects around here. Go ahead and try looting some XP!";
    }

    private void FinalPrompt()
    {
        portal.SetActive(true);
        messageText.text = "Good job! You've finished the tutorial. Don't forget to spend your newly acquired XP! To proceed, go up the stairs. To pause the game, you can press 'Escape' at any moment.";
    }
}
