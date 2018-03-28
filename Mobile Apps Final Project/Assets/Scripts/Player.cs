using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: check for collision and don't add to movement count
public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    public LayerMask blockingLayer;
    public int moveSpeed;
    public float jumpHeight = 1.2f;
    public float maxMovement;
    public Transform groundCheck;

    private float movementCount;   
    private Vector2 wallCheck;
    private float move = 0.0f;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private bool isGrounded;
    private bool isWall;
    private bool jump;

    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Sets move to 0 on every frame
        move = 0.0f;
    }

    //Update for physics
    void FixedUpdate()
    { 
      
        //Uses Linecast to see if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, jumpHeight, blockingLayer);

        //Checks if the jump button was pressed and if the player is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }

        //If d or a is pressed then call move function
        move = Input.GetAxis("Horizontal");
        wallCheck = new Vector2(rb2D.position.x + move, rb2D.position.y);
        isWall = Physics2D.Linecast(rb2D.position, wallCheck, blockingLayer);

        if(isWall)
        {
            return;
        }
        else
        {
            Debug.Log(move);
            Move(move);
        }
       

        //If player is grounded and jump is true then perform jump movement
        if (jump)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight * moveSpeed);
            jump = false;
        }
    }

    private void Move(float moveDir)
    {
        movementCount = movementCount + moveDir;
        Debug.Log(movementCount);
        if (Mathf.Abs(movementCount) > maxMovement)
        {
            if (movementCount < 0)
            {
                movementCount = -maxMovement;
            }
            else
            {
                movementCount = maxMovement;
            }

            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0.0f;
            return;
        }
        else
        {
            rb2D.velocity = new Vector2(moveDir * moveSpeed, rb2D.velocity.y);
        }
    }
}
