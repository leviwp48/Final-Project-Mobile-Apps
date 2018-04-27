using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


//[System.Serializable]
//TODO: check for collision and don't add to movement count
public class Player2: MonoBehaviour
{
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private Transform snowBallPos;
	[SerializeField]
	private LayerMask waterLayer;
	[SerializeField]
	private LayerMask rockLayer;
	[SerializeField]
	private LayerMask lavaLayer;
	[SerializeField]
	private LayerMask blockingLayer;

	public int moveSpeed;
	public float groundCheckSize;
	public float jumpPower;
	public float maxMovement;
	public float maxHealth;

	public Transform groundCheck;

	[HideInInspector]
	public float currHealth;
	[HideInInspector]
	public float movementCount; 


	public bool isFacingLeft;
	private Shoot shootScript; 

	private Vector2 newPos;
	private SpriteRenderer playerSprite;
	private Vector2 wallCheck;
	public float move = 0.0f;
	private Rigidbody2D rb2D;
	private bool isGrounded;
	private bool isWall;
	private bool jump;
	private Animator anim;
	private bool isGroundedWater;
	private bool isGroundedRock;
	private bool isGroundedLava;

	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start()
	{
		//Get a component reference to this object's BoxCollider2D

		anim = GetComponent<Animator> ();
		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent<Rigidbody2D>();

		playerSprite = GetComponent<SpriteRenderer>();

		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();

		isFacingLeft = true;
		currHealth = maxHealth;
	}

	void Update()
	{		
        if (GameManager.instance.p2Turn == true)
        {
           // move = Input.GetAxis("Horizontal");

            if (move != 0 && !shootScript.isAiming)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            //Checks if the jump button was pressed and if the player is on the ground
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                jump = true;
            }
			else if (Input.GetButtonDown("Jump") && isGroundedWater)
			{
				jump = true;
			}
			else if (Input.GetButtonDown("Jump") && isGroundedLava)
			{
				jump = true;
			}
			else if (Input.GetButtonDown("Jump") && isGroundedRock)
			{
				jump = true;
			}

            if (isFacingLeft)
            {
                newPos = new Vector2(player.transform.position.x - 4.5f, snowBallPos.position.y);
                snowBallPos.position = newPos;
            }
            else if (!isFacingLeft)
            {
                newPos = new Vector2(player.transform.position.x + 4.5f, snowBallPos.position.y);
                snowBallPos.position = newPos;
            }
        }
	}

	//Update for physics
	void FixedUpdate()
	{
        if (GameManager.instance.p2Turn == true)
        {
			if (!shootScript.isAiming && !shootScript.isThrown)
            {
                //Uses Linecast to see if the player is on the ground
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, blockingLayer);
				isGroundedWater = Physics2D.OverlapCircle(groundCheck.position, 1f, waterLayer);
				isGroundedRock = Physics2D.OverlapCircle(groundCheck.position, 1f, rockLayer);
				isGroundedLava = Physics2D.OverlapCircle(groundCheck.position, 1f, lavaLayer);

                //If d or a is pressed then call move function

                if (move > 0 && isFacingLeft && !shootScript.isAiming)
                {
                    playerSprite.flipX = true;
                    isFacingLeft = false;
                }
                else if (move < 0 && !isFacingLeft && !shootScript.isAiming)
                {
                    playerSprite.flipX = false;
                    isFacingLeft = true;
                }

              
                Move(move);
                


                //If player is grounded and jump is true then perform jump movement
                if (jump)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower * moveSpeed);
					rb2D.gravityScale = 15f;
                    jump = false;
                }
            }
		}
	}

	private void Move(float moveDir)
	{
		Debug.Log ("moving");
		movementCount = movementCount + moveDir;
		if (Mathf.Abs (movementCount) > maxMovement)
		{
			if (movementCount < 0) 
			{
				Debug.Log ("moving");
				movementCount = -maxMovement;
			}
			else 
			{
				movementCount = maxMovement;
			}
		}	
		else if(Mathf.Abs(movementCount) < maxMovement)
		{
			Vector2 boxVectorStartRight = new Vector2 (transform.position.x + 2f, transform.position.y);
			Vector2 boxVectorStartLeft = new Vector2 (transform.position.x - 2f, transform.position.y);
			if (moveDir > 0) 
			{
				if (Physics2D.OverlapCircle (boxVectorStartRight, 1f, blockingLayer))
				{
					Debug.Log ("stopping");
				} 
				else 
				{
					rb2D.velocity = new Vector2 (moveDir * moveSpeed, rb2D.velocity.y);
				}
			} 
			else if (moveDir < 0)
			{
				if (Physics2D.OverlapCircle (boxVectorStartLeft, 1f, blockingLayer))
				{
					Debug.Log ("stopping");
				} 
				else 
				{
					rb2D.velocity = new Vector2 (moveDir * moveSpeed, rb2D.velocity.y);
				}
			}		
		}
	}
}
