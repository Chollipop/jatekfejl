using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private static HealthBar healthBarInstance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (healthBarInstance == null)
        {
            healthBarInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
