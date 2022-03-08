using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodyPickup : MonoBehaviour
{
    public enum PickupType {Health, Speedup, FireRate}
    public PickupType pickupType;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerController>().inactive)
            {
                if (pickupType == PickupType.Health)
                {
                    collision.GetComponent<Health>().PickupHealth();
                }

                if (pickupType == PickupType.Speedup)
                {
                    collision.GetComponent<PlayerController>().SpeedUp();
                }

                if (pickupType == PickupType.FireRate)
                {
                    collision.GetComponentInChildren<Shooting>().PickupFireRate();
                }
                Destroy(gameObject);
            }
        }

    }
}
