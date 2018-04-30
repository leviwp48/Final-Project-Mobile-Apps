using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;


//[System.Serializable]
//TODO: check for collision and don't add to movement count
public class Player1: MonoBehaviour
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
    [SerializeField]
    public Stat health;

	public int moveSpeed;
	public float groundCheckSize;
    public float jumpPower;
    public float maxMovement;
	//public float maxHealth;

	public Transform groundCheck;

	public float maxHealth;
	public float currHealth;
	[HideInInspector]
	public float movementCount;   


	public bool isFacingLeft;
	private Shoot shootScript; 

	private Vector2 newPos;
	private SpriteRenderer playerSprite;
	private Vector2 wallCheck;
	public float move = 0.0f;
	public float jumpMove = 0.0f;
	private Rigidbody2D rb2D;
	private bool isGrounded;
	private bool isWall;
	private bool jump;
	private Animator anim;
	private bool isGroundedWater;
	private bool isGroundedRock;
	private bool isGroundedLava;

    private void Awake()
    {
        //starts the health bars before starting the player and stuff otherwise it doesn't work
        //fills health bar and starts tracking health bar numbers and sets current and maximum values
        health.Initialize();
    }

	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start()
	{
		//Get a component reference to this object's BoxCollider2D


		anim = GetComponent<Animator> ();
		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent<Rigidbody2D>();

		playerSprite = GetComponent<SpriteRenderer>();

		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();

		currHealth = maxHealth;
        //isFacingLeft = true;
	}

	void Update()
	{		
        if (GameManager.instance.p1Turn)
        {
          
            Debug.Log("move:");
			Debug.Log(move);
			if (!shootScript.isAiming && !shootScript.isThrown)
            {
				move = 0;
				jumpMove = 0;
				move = CrossPlatformInputManager.GetAxis("Horizontal");
				jumpMove = CrossPlatformInputManager.GetAxis("Vertical");

                if (move != 0) {
					anim.SetBool ("isMoving", true);
				} else {
					anim.SetBool ("isMoving", false);
				}

				if (move > 0 && isFacingLeft) {
					playerSprite.flipX = true;
					isFacingLeft = false;
				} else if (move < 0 && !isFacingLeft) {
					playerSprite.flipX = false;
					isFacingLeft = true;
				}

				//Checks if the jump button was pressed and if the player is on the ground
				if (jumpMove > 0 && isGrounded)
				{
					jump = true;
				}
				else if (jumpMove > 0 && isGroundedWater)
				{
					jump = true;
				}
				else if (jumpMove > 0 && isGroundedLava)
				{
					jump = true;
				}
				else if (jumpMove > 0 && isGroundedRock)
				{
					jump = true;
				}

				if (isFacingLeft) {
					newPos = new Vector2 (player.transform.position.x - 4.5f, snowBallPos.position.y);
					snowBallPos.position = newPos;
				} else if (!isFacingLeft) {
					newPos = new Vector2 (player.transform.position.x + 4.5f, snowBallPos.position.y);
					snowBallPos.position = newPos;
				}
			}
		}
	}

	//Update for physics
	void FixedUpdate()
	{
        if (GameManager.instance.p1Turn)
        {
            if (!shootScript.isAiming && !shootScript.isThrown)
            {
                //Uses Linecast to see if the player is on the ground
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, blockingLayer);
				isGroundedWater = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, waterLayer);
				isGroundedRock = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, rockLayer);
				isGroundedLava = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, lavaLayer);

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

    //Using for Final Presentation
	private void Move(float moveDir)
	{
        // Keeps track of how far we've moved
		movementCount = movementCount + moveDir;
        
        // Checks if we are at a maximum left or maximum right
        // If at left set movementCount to -maxMovement
        // If at right set movementCount to maxMovement
		if (Mathf.Abs (movementCount) > maxMovement) {
			if (movementCount < 0) {
				movementCount = -maxMovement;
			} else {
				movementCount = maxMovement;
			}
		}	
		else 
        // If we are not at a maximum
		{
            // Creates boxes to the left and to the right of the player's current position
			Vector2 boxVectorStartRight = new Vector2 (transform.position.x + 2f, transform.position.y);
			Vector2 boxVectorStartLeft = new Vector2 (transform.position.x - 2f, transform.position.y);

            // If our move input is greater than 0 (moving to the right)
			if (moveDir > 0) 
			{
                // Checks if the box to the right is in contact with a wall
                // If so then do nothing
                // Else move as usual
				if (Physics2D.OverlapCircle (boxVectorStartRight, 1f, blockingLayer))
				{
                    // This stops the player from slowly gliding down walls when in contact
                    // It took a while to realize that I needed to do nothing
					Debug.Log ("stopping");
				} 
				else 
				{
					rb2D.velocity = new Vector2 (moveDir * moveSpeed, rb2D.velocity.y);

				}
			}

            // Checks if the box to the left is in contact with a wall
            // If so then do nothing
            // Else move as usual
            else if (moveDir < 0)
			{
				if (Physics2D.OverlapCircle (boxVectorStartLeft, 1f, blockingLayer))
				{

				} 
				else 
				{
					rb2D.velocity = new Vector2 (moveDir * moveSpeed, rb2D.velocity.y);
				}
			}		
		}
	}
}
