using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    float spreadRadius = 30;



   
  
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Instance"))
        {
            Debug.Log("Reposition");
            Reposition();

        }
       
    }

   



    void Reposition()
    {
        transform.position = new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0);
    }
}
