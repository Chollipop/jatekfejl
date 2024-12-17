using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;
    private List<FloatingText> floatingTexts = new List<FloatingText>();
    private static FloatingTextManager floatingTextManagerInstance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (floatingTextManagerInstance == null)
        {
            floatingTextManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        foreach (var text in floatingTexts)
        {
            text.UpdateFloatingText();
        }
    }

    private FloatingText GetFloatingText()
    {
        FloatingText text = floatingTexts.Find(x => !x.active);
        if (text == null)
        {
            text = new FloatingText();
            text.go = Instantiate(textPrefab);
            text.go.transform.SetParent(textContainer.transform);
            text.text = text.go.GetComponent<Text>();
            floatingTexts.Add(text);
        }
        return text;
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;
        floatingText.Show();
    }
}