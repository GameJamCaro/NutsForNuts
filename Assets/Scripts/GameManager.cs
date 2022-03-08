using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    int playerScore;

    public ScoreDisplay[] scoreUIs;




    private void Awake()
    {
        // When this component is first added or activated, setup the global reference
        if (instance == null)
        {
            instance = this;
        }
        else
        {
           Destroy(this.gameObject);
        }
    }
    public static int score
    {
        get
        {
            return instance.playerScore;
        }
        set
        {
            instance.playerScore = value;
        }
    }

    public static void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        GameManager.UpdateUI();    
        
    }

    public static void UpdateUI()
    {
        Debug.Log("Update UI");
        instance.scoreUIs[0].DisplayScore();
    }

    
}
