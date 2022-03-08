using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    bool isMoving;
    private Rigidbody2D rb;
    private Vector3 move;
    private float playerSpeed;
    public bool inactive;

    //Flag to use Rigidbody movement. Default to true for optimal performance, 
    //If the flag is false this script will update the transform directly.
    public bool isRigidBody = true;

    //Scaler for player Speed
    [Range(0, 10)]
    public float startSpeed;
    public float speedUpSpeed;

    public Texture2D reticle;

    public Inventory inventoryScript;

   

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerSpeed = startSpeed;
        Cursor.SetCursor(reticle, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void TogglePlayerMovement(bool isMoving)
    {
        if (isMoving)
        {
            playerSpeed = startSpeed;
        }
        else
        {
            playerSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (true)
        {
            //INPUT
            move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            move = (move.magnitude > 1.0f) ? move = move.normalized : move;

            //MOVEMENT
            if (!isRigidBody)
            {
                transform.position += move * Time.deltaTime * playerSpeed;
            }
            else
            {
                if (!isMoving)
                {
                    rb.velocity = Vector3.zero;
                }
                else
                {
                    rb.velocity = move * playerSpeed;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public float speedUpTime = 5;
    public void SpeedUp()
    {
        StartCoroutine(SpeedUpTimer());
    }

    IEnumerator SpeedUpTimer()
    {
        playerSpeed = speedUpSpeed;
        yield return new WaitForSeconds(speedUpTime);
        playerSpeed = startSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !inactive)
        {
            GetComponent<Health>().TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
