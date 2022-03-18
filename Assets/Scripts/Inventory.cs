using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;



public class Inventory : MonoBehaviour
{
    public List<System.Tuple<Sprite, int>> foodList;

    public GameObject[] foodUIs;

    AudioSource audioSource;
    public AudioClip[] audioClips;

    bool digesting;
    WaitForSeconds blinkWait;

    public Health healthScript;
    public TimeManager timerScript;

  

   
  

    private void Start()
    {
        blinkWait = new WaitForSeconds(.5f);

        foreach(GameObject ui in foodUIs)
        {
            ui.GetComponent<Image>().enabled = false;
        }
        foodList = new List<System.Tuple<Sprite, int>>();
        audioSource = GetComponent<AudioSource>();
    }


    bool once;
    private void Update()
    {
        // if there is no food left, player begins to lose health
        if (foodList.Count < 1 && !once)
        {
            StartCoroutine(LoseHeart());
            once = true;
        }

        if (timerScript.win)
        {

            FillInventory();
            if (timerScript.win)
            {
                foodUIs[0].GetComponent<Image>().enabled = true;
            }
        }
    }

    public void NewEntry(Sprite image, int digestionTime)
    {
        once = false;
        if (foodList.Count < foodUIs.Length)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length - 1)];
            audioSource.Play();

            foodList.Add(System.Tuple.Create(image, digestionTime));

            FillInventory();

            if (!digesting)
                StartCoroutine(Digest());


        }
        
    }


    void ClearInventory()
    {
        foreach (GameObject ui in foodUIs)
        {
            ui.GetComponent<Image>().enabled = false;
            ui.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    void FillInventory()
    {
        ClearInventory();

        for(int i = 0; i < foodList.Count; i++)
        {
                foodUIs[i].GetComponent<Image>().enabled = true;
                foodUIs[i].GetComponent<Image>().sprite = foodList.ElementAt(i).Item1;
        }
        ShowPoints();
            
    }

    public void ShowPoints()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            
            foodUIs[i].GetComponentInChildren<TextMeshProUGUI>().text = foodList.ElementAt(i).Item2.ToString();
        }
    }


    public int digestCounter;
    IEnumerator Digest()
    {
        if (foodList.Count > 0)
        {
            digesting = true;
           


            digestCounter = foodList[0].Item2;
            for (int i = digestCounter; i > 0; i--)
            {
                if (!timerScript.win)
                {
                    foodUIs[0].GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
                    foodUIs[0].GetComponent<Image>().enabled = false;
                    yield return blinkWait;
                    foodUIs[0].GetComponent<Image>().enabled = true;
                    yield return blinkWait;
                }
                else
                    foodUIs[0].GetComponent<Image>().enabled = true;

            }
            if (!timerScript.win)
            {
                foodList.RemoveAt(0);
                FillInventory();

                digesting = false;


                StartCoroutine(Digest());
            }
        }

    }

    

    IEnumerator LoseHeart()
    {
        for (int i = 0; i < 5; i++)
        {
            if (foodList.Count < 1)
            {
                healthScript.healthIcons[healthScript.healthIcons.Length - 1].GetComponent<Image>().enabled = false;
                yield return blinkWait;
                healthScript.healthIcons[healthScript.healthIcons.Length - 1].GetComponent<Image>().enabled = true;
                yield return blinkWait;
            }
        }
        if (foodList.Count < 1)
        {
            healthScript.TakeDamage(1,0);
            StartCoroutine(LoseHeart());
        }

    }


    public int GetFinalScore()
    {
        digesting = false;
        StopAllCoroutines();
        int finalScore = 0;
        foreach(System.Tuple<Sprite,int> food in foodList)
        {
            finalScore += food.Item2;
        }

        if (!PlayerPrefs.HasKey("HighScore") || PlayerPrefs.GetInt("HighScore") < finalScore)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
        }

        Debug.Log("foodlist count:  " + foodList.Count);
        return finalScore;
    }

    

   
}
