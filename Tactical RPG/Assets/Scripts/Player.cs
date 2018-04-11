using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//TODO: check for collision and don't add to movement count
public class Player
{

	[SerializeField]
	private GameObject player;
	[SerializeField]
	private Transform snowBallPos;

	public LayerMask blockingLayer;
	public int moveSpeed;
	public float jumpHeight = 1.2f;
	public float maxMovement;
	public float maxHealth;
    public float currHealth;
	public Transform groundCheck;

	public bool isFacingLeft;

	private Shoot shootScript;  
	private Vector2 newPos;
	private SpriteRenderer playerSprite;
	private float movementCount;   
	private Vector2 wallCheck;
	private float move = 0.0f;
	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private bool isGrounded;
	private bool isWall;
	private bool jump;
	private Animator anim;

	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start()
	{
		//Get a component reference to this object's BoxCollider2D
		boxCollider = GetComponent<BoxCollider2D>();

		anim = GetComponent<Animator> ();
		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent<Rigidbody2D>();

		playerSprite = GetComponent<SpriteRenderer>();

		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();

		isFacingLeft = true;
	}

	void Update()
	{
		if (move == 0) {
			anim.SetBool ("canMove", false);
		} else {
			anim.SetBool ("canMove", true);
		}
		//Sets move to 0 on every frame
		move = 0.0f;

		//Checks if the jump button was pressed and if the player is on the ground
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			jump = true;
		}

		if(isFacingLeft)
		{           
			newPos = new Vector2(player.transform.position.x - 4.5f, snowBallPos.position.y);
			snowBallPos.position = newPos;
		}
		else if(!isFacingLeft)
		{
			newPos = new Vector2(player.transform.position.x + 4.5f, snowBallPos.position.y);
			snowBallPos.position = newPos;
		}

	}

	//Update for physics
	void FixedUpdate()
	{ 
		if (!shootScript.isAiming) {
			//Uses Linecast to see if the player is on the ground
			isGrounded = Physics2D.OverlapCircle (groundCheck.position, jumpHeight, blockingLayer);

			//If d or a is pressed then call move function
			move = Input.GetAxis ("Horizontal");

			if (move > 0 && isFacingLeft) {
				playerSprite.flipX = true;
				isFacingLeft = false;
			} else if (move < 0 && !isFacingLeft) {
				playerSprite.flipX = false;
				isFacingLeft = true;
			}

			wallCheck = new Vector2 (rb2D.position.x + move, rb2D.position.y);
			isWall = Physics2D.Linecast (rb2D.position, wallCheck, blockingLayer);

			if (isWall) {
				return;
			} else {   
				Move (move);
			}


			//If player is grounded and jump is true then perform jump movement
			if (jump) {
				rb2D.velocity = new Vector2 (rb2D.velocity.x, jumpHeight * moveSpeed);
				jump = false;
			}
		}
	}

	private void Move(float moveDir)
	{
		movementCount = movementCount + moveDir;
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
