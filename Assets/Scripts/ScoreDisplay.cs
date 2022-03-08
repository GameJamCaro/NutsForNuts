using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{

    [Header("References")]
    [Tooltip("The text to use when displaying the score")]
    public TextMeshProUGUI displayText = null;

    public int scoreID;


    private void Start()
    {
        DisplayScore();
    }
    public void DisplayScore()
    {
        if (displayText != null)
        {
            if (scoreID == 0)
                displayText.text = "score: " + GameManager.score;
           

        }
    }
}
