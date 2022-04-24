using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TestHealthText : MonoBehaviour 
{
    public Health playerHealth;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (playerHealth)
        {
            text.text = string.Format("Health: " + playerHealth.healthValue);
        }
    }

    public void getPlayerHealth()
    {
        playerHealth = GameManager.Instance.thePlayer.playerHealth;
    }
} 