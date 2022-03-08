using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhealth = 5;
    public int currentHealth;
    public GameObject[] healthIcons;
    public GameObject deathPanel;
    AudioSource audioSource;
    public AudioClip[] hurtSounds;
  
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxhealth;
        DisplayHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayHealth()
    {
        foreach (GameObject icon in healthIcons)
        {
            icon.SetActive(false);
        }
        for (int i = 0; i < currentHealth; i++)
        {
            healthIcons[i].SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioSource.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
        audioSource.Play();
        DisplayHealth();
        if(currentHealth < 1)
        {
            deathPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PickupHealth()
    {
        currentHealth += 1;
        DisplayHealth();
       
    }


}
