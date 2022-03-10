using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetation : MonoBehaviour
{
    public Sprite[] seasonSprites;
    


    public void SetSeasonSprite(int spriteId)
    {
       
       
        GetComponent<SpriteRenderer>().sprite = seasonSprites[spriteId];
    }
}
