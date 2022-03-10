using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodyPickup : MonoBehaviour
{
    public enum PickupType {Health, Speedup, FireRate}
    public PickupType pickupType;

    AudioSource audioSource;
    public AudioClip collectionSound;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = collectionSound;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.pitch = Random.Range(.5f, 1.5f);
            if (!collision.GetComponent<PlayerController>().inactive)
            {
                if (pickupType == PickupType.Health)
                {
                    collision.GetComponent<Health>().PickupHealth(gameObject);
                }

                if (pickupType == PickupType.Speedup && !collision.GetComponent<PlayerController>().speeding)
                {
                    collision.GetComponent<PlayerController>().SpeedUp();
                    audioSource.Play();
                    GetComponent<SpriteRenderer>().enabled = false;
                    Destroy(gameObject, 2);

                }

                if (pickupType == PickupType.FireRate && !collision.GetComponentInChildren<Shooting>().highFireRate)
                {
                    collision.GetComponentInChildren<Shooting>().PickupFireRate();
                    audioSource.Play();
                    GetComponent<SpriteRenderer>().enabled = false;
                    Destroy(gameObject, 2);
                }
                
               
               
               
            }
        }

    }
}
