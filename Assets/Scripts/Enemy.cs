using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip[] deathSounds;
    public Transform firePoint;
    GameObject player;
    public GameObject projectile;
    int force = 5;

    bool shooting;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitBeforeShooting());
        
    }


    bool once1;
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(startPosition, player.transform.position) < 10 && !once1 && !player.GetComponent<PlayerController>().inactive && shooting)
        {
            StartCoroutine(WaitAndShoot());
            Debug.Log("Shoot Distance");
            once1 = true;
        }
        else
            StopCoroutine(WaitAndShoot());
    }

    bool once;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Player"))
        {
            Debug.Log("enemy");
            collision.gameObject.GetComponent<Health>().TakeDamage(1);

        }
        */
        if(collision.CompareTag("Projectile") && !once)
        {
            once = true;
            // GameManager.AddScore(1);
            GetComponent<SpriteRenderer>().enabled = false;
            audioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
            audioSource.Play();
            Destroy(gameObject,1);

        }
    }


   void Shoot()
    {
        firePoint.transform.LookAt(player.transform);
        var bullet = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
        Debug.Log(bullet.transform);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.forward * force, ForceMode2D.Impulse);
        Destroy(bullet, 1);
    }

    IEnumerator WaitAndShoot()
    {
        Shoot();
        yield return new WaitForSeconds(1);
        StartCoroutine(WaitAndShoot());
    }


    IEnumerator WaitBeforeShooting()
    {
        yield return new WaitForSeconds(2);
        shooting = true;

    }
}
