using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float fireRate = 0;
    public bool highFireRate;
    public float pickUpFireRate = 10;
    public float damage = 10;
    public LayerMask whatToHit;


    float timeToFire = 0;
    public GameObject firePoint;
    public GameObject projectile;
    public float force = 100;
    public Transform armRot;

    AudioSource audioSource;
    public AudioClip shotSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shotSound;
        
        if(firePoint == null)
        {
            Debug.Log("No firepoint");
        }
    }

    private void Update()
    {
        if(fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    
    private void Shoot()
    {
        audioSource.Play();
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector3 firePointPosition = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y,0);
        GameObject projectileInst = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
        projectileInst.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * force, ForceMode2D.Impulse);
        Destroy(projectileInst, .7f);
        
    }

    public void PickupFireRate()
    {
        StartCoroutine(FireRateTimer());
    }

    private IEnumerator FireRateTimer()
    {
        fireRate = pickUpFireRate;
        highFireRate = true;
        yield return new WaitForSeconds(10);
        fireRate = 0;
        highFireRate = false;
    }
}
