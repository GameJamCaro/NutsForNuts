using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] nuts;
    public GameObject[] freshFood;
    public GameObject[] vegetation;
    public GameObject[] enemies;
    public GameObject[] goodies;

    List<GameObject> vegetationInstances;

    

    public int spreadRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        vegetationInstances = new List<GameObject>();
        SpreadVegetation(30);
        // CheckDistance();
        SpreadGoodies(5);
       
    }

    public void CheckDistance()
    {
        for(int i = 0; i < vegetationInstances.Count-2; i++)
        {
           // Debug.Log(Vector2.Distance(vegetationInstances[i].transform.position, vegetationInstances[i + 1].transform.position));
            if(Vector2.Distance(vegetationInstances[i].transform.position, vegetationInstances[i+1].transform.position) < 20)
            {
                vegetationInstances[i].transform.position = new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0);
                Debug.Log("Reposition");
                CheckDistance();
            }
        }
    }

    void Reposition()
    {
       
    }

    public void SpreadNuts(int nutNumber)
    {
        for (int i = 0; i < nutNumber; i++)
        {
            var nut = Instantiate(nuts[Random.Range(0, nuts.Length - 1)], new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(nut, 40);
        }
       
        

    }

    public void SpreadFreshFood(int freshFoodNumber)
    {
        
        for (int i = 0; i < freshFoodNumber; i++)
        {
            var veg = Instantiate(freshFood[Random.Range(0, freshFood.Length - 1)], new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(veg, 20);
        }
    }

    void SpreadVegetation(int vegetationNumber)
    {

        for (int i = 0; i < vegetationNumber; i++)
        {
            var vegetationInst = Instantiate(vegetation[0], new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0), Quaternion.identity);
            vegetationInstances.Add(vegetationInst);
            
          }

    }

   


    public void SpreadEnemies(int enemyNumber)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
        


    }

    public void SpreadGoodies(int goodyNumber)
    {
        for (int i = 0; i < goodyNumber; i++)
        {
            Instantiate(goodies[Random.Range(0, goodies.Length)], new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0), Quaternion.identity);
        }



    }
}
