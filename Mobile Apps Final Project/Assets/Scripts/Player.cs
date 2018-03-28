using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject snowBall;

    public LayerMask blockingLayer;
    public int moveSpeed;
    public float jumpHeight = 1.2f;

    private Vector2 groundCheck;
    private float move = 0.0f;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb2D;
    private bool isGrounded;
    private bool jump;

    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        circleCollider = GetComponent<CircleCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Sets move to 0 on every frame
        move = 0.0f;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject snowBallInstance =
                Instantiate(snowBall, player.transform.position, Quaternion.identity) as GameObject;

            snowBallInstance.transform.position = new Vector2(transform.position.x + 5, transform.position.y + 2);
        }
    }

    //Update for physics
    void FixedUpdate()
    {
        //End position for player to check if they're on the ground
        groundCheck = new Vector2(rb2D.position.x, rb2D.position.y - 0.75f);

        //Uses Linecast to see if the player is on the ground
        isGrounded = Physics2D.Linecast(rb2D.position, groundCheck, blockingLayer);

        //Checks if the jump button was pressed and if the player is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            jump = true;
        }

        //If d or a is pressed then call move function
        move = Input.GetAxis("Horizontal");
        Move(move);

        //If player is grounded and jump is true then perform jump movement
        if (jump)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHeight * moveSpeed);
            jump = false;
        }
    }

    private void Move(float moveDir)
    {
        rb2D.velocity = new Vector2(moveDir * moveSpeed, rb2D.velocity.y);
    }

}
