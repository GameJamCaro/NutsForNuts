using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;



public class Inventory : MonoBehaviour
{
    List<System.Tuple<Sprite, int>> foodList;

    public GameObject[] foodUIs;

    AudioSource audioSource;
    public AudioClip[] audioClips;

    bool digesting;
    WaitForSeconds blinkWait;

    public Health healthScript;

  

   
  

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
        if(foodList.Count < 1 && !once)
        {
            StartCoroutine(LoseHeart());
            once = true;
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
        }
    }

    void FillInventory()
    {
        ClearInventory();

        for(int i = 0; i < foodList.Count; i++)
        {
            if (i < foodUIs.Length)
            {
                foodUIs[i].GetComponent<Image>().enabled = true;
                foodUIs[i].GetComponent<Image>().sprite = foodList.ElementAt(i).Item1;
                                }
        }
        
            
    }

 
    IEnumerator Digest()
    {
        digesting = true;
       
        for(int i = 0; i < foodList[0].Item2; i++)
        {
            foodUIs[0].GetComponent<Image>().enabled = false;
            yield return blinkWait;
            foodUIs[0].GetComponent<Image>().enabled = true;
            yield return blinkWait;
        }
        
        foodList.RemoveAt(0);
        FillInventory();
        digesting = false;
        if(foodList.Count < 1)
        {
            StartCoroutine(LoseHeart());
        }
        else
        {
            StopCoroutine(LoseHeart());
        }
        
        StartCoroutine(Digest());

    }

    

    IEnumerator LoseHeart()
    {
        for (int i = 0; i < 5; i++)
        {
            healthScript.healthIcons[healthScript.healthIcons.Length-1].GetComponent<Image>().enabled = false;
            yield return blinkWait;
            healthScript.healthIcons[healthScript.healthIcons.Length-1].GetComponent<Image>().enabled = true;
            yield return blinkWait;
        }
        if (foodList.Count < 1)
        {
            healthScript.TakeDamage(1);
            StartCoroutine(LoseHeart());
        }

    }


    public int GetFinalScore()
    {
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

        return finalScore;
    }

    

   
}
