using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI seasonUI;
    public TextMeshProUGUI finalScoreUI;
    public TextMeshProUGUI highScoreUI;
    public int seasonTime;
    int tempTime;
    public int season;
    public GameObject winPanel;
    public Camera cam;

    AudioSource audioSource;
    public AudioClip summerAmbient;
    public AudioClip fallAmbient;
    public AudioClip winterAmbient;
    public AudioClip springAmbient;

    public Color springColor;
    public Color summerColor;
    public Color fallColor;
    public Color winterColor;

    public GameObject seasonPanel;
    public TextMeshProUGUI seasonText;

    string currentSeason;

    LevelGenerator genScript;
    public PlayerController playerScript;
    public Inventory inventoryScript;

    public bool win;








    private void Start()
    {
       
        genScript = GetComponent<LevelGenerator>();
        audioSource = GetComponent<AudioSource>();

        tempTime = seasonTime;
        SetSeason();
        season = 0;
        
        StartCoroutine(CountDown());
      
    }

    private void Update()
    {
        if(tempTime == 0)
        {
            season++;
            tempTime = seasonTime;
            timeUI.text = tempTime.ToString();
            SetSeason();
        }
    }

    void SetSeason()
    {
        switch(season)
        {
            case 0:
                
                currentSeason = "Summer";
                audioSource.clip = summerAmbient;
                seasonPanel.GetComponent<Image>().color = summerColor;
                seasonUI.text = currentSeason;
                cam.backgroundColor = summerColor;
                genScript.SpreadFreshFood(7);
                genScript.SpreadNuts(3);
                genScript.SpreadEnemies(15);
                StartCoroutine(SeasonInfo());

                break;
            case 1:
                genScript.SetVegetationSeason(0);
                currentSeason = "Fall";
                audioSource.clip = fallAmbient;
                seasonPanel.GetComponent<Image>().color = fallColor;
                seasonUI.text = currentSeason;
                cam.backgroundColor = fallColor;
                genScript.SpreadEnemies(10);
                genScript.SpreadNuts(15);
                genScript.SpreadGoodies(3);
                StartCoroutine(SeasonInfo());
                break;
            case 2:
                genScript.SetVegetationSeason(1);
                currentSeason = "Winter";
                audioSource.clip = winterAmbient;
                seasonPanel.GetComponent<Image>().color = winterColor;
                seasonPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                seasonUI.text = currentSeason;
                cam.backgroundColor = winterColor;
                genScript.SpreadEnemies(15);
                genScript.SpreadGoodies(5);
                StartCoroutine(SeasonInfo());
                break;
            case 3:
                win = true;
                inventoryScript.ShowPoints();
                StopAllCoroutines();
                Time.timeScale = 0;
                audioSource.clip = springAmbient;
                winPanel.SetActive(true);
                finalScoreUI.text = "Nutrition saved: " + inventoryScript.GetFinalScore();
                highScoreUI.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
                break;

        }

        audioSource.Play();



    }



    public IEnumerator CountDown()
    {
        timeUI.text = tempTime.ToString();
        yield return new WaitForSeconds(1);
        tempTime--;
        StartCoroutine(CountDown());
    }

    public IEnumerator SeasonInfo()
    {
        playerScript.inactive = true;
        seasonPanel.SetActive(true);
        seasonText.text = currentSeason;
        yield return new WaitForSeconds(1);
        seasonPanel.SetActive(false);
        yield return new WaitForSeconds(1);
        playerScript.inactive = false;

    }
}
