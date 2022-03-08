using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodPickup : MonoBehaviour
{
    public int digestionTime = 10;
    Animator anim;
    bool once;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !once)
        {
            if (!collision.GetComponent<PlayerController>().inactive)
            {
                once = true;
                collision.gameObject.GetComponent<PlayerController>().inventoryScript.NewEntry(GetComponent<SpriteRenderer>().sprite, digestionTime);
                anim.SetTrigger("bounce");
                Destroy(gameObject, .5f);
            }
            
        }
    }

}
