using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeaponAction : MonoBehaviour {

	//LayerMasks to detect certain layers (Player & Ground)
	[SerializeField]
	private LayerMask playerLayer;
	[SerializeField]
	private LayerMask blockingLayer;
	[SerializeField]
	private LayerMask waterLayer;
	[SerializeField]
	private LayerMask rockLayer;
	[SerializeField]
	private LayerMask lavaLayer;

	private Transform groundCheck;

	//Script to control the weapon
    private Shoot shootScript;
	//Player scripts to switch between players
	private Player1 playerScript;
	private Player2 playerScript2;
	//Animates the weapon
	private Animator anim;
	//Counts the times the Grenade has bounces
	private int bounceCount;
	//Will be True if the playerLayer or blockingLayer was hit
	private bool hitsPlayer;
	private bool hitsGround;
	//Accesses tilemap to delete tiles
	private Tilemap tilemap;
	//Accesses grid to grab cell positions
	private Grid grid;
	private Vector3 oldPos;
	private bool isStuck;
	private int timer;
	private bool isGrounded;
	private bool isGroundedWater;
	private bool isGroundedRock;
	private bool isGroundedLava;

    

	//May not be necessary. But helps in filtering what we contact
    public ContactFilter2D contactFilter;

	//Is True if the player is aiming and the weapon hits the ground
	public bool maxRight;
	public bool maxLeft;

	//Is True if we want to destroy tiles
	public bool canDestroy;

	//On awake, the weapon will initialize the bouncecount to 0 if its a grenade.
	//It will set the tilemap, animator, shootScript, grid, and contact filter.
	void Awake()
	{
		if (gameObject.name == "Grenade") {
			bounceCount = 0;
			timer = 0;
		}

		oldPos = Vector3.zero;
		tilemap = GameObject.Find("Map").GetComponent<Tilemap> ();
		anim = this.GetComponent<Animator>();
		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
		grid = GameObject.Find("Grid").GetComponent<Grid> ();
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(blockingLayer);
		groundCheck = GetComponentInChildren<Transform>();
	}

	//On every frame, check if its player one or player two's turn and grabs their scripts respectively.
	void Update()
	{
		if (GameManager.instance.p1Turn) {
			playerScript2 = GameObject.Find ("Player2").GetComponent<Player2> ();
		}
		else if(GameManager.instance.p2Turn) {
			playerScript = GameObject.Find ("Player1").GetComponent<Player1> ();
		}

		if (shootScript.isThrown) {
			if (bounceCount > 0) {
				Debug.Log ("bounce count is");

				Debug.Log (bounceCount);
				timer++;
				Debug.Log ("timer is");
				Debug.Log (timer);



				if (timer > 50) {
					isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1f, blockingLayer);
					isGroundedWater = Physics2D.OverlapCircle(groundCheck.position, 1f, waterLayer);
					isGroundedRock = Physics2D.OverlapCircle(groundCheck.position, 1f, rockLayer);
					isGroundedLava = Physics2D.OverlapCircle(groundCheck.position, 1f, lavaLayer);
					if(isGrounded)
					{
						Invoke ("Explode", 0.1f);	
					}
					else if(isGroundedWater)
					{
						Invoke ("Explode", 0.1f);	
					}
					else if(isGroundedRock)
					{
						Invoke ("Explode", 0.1f);	
					}
					else if(isGroundedLava)
					{
						Invoke ("Explode", 0.1f);	
					}
				}
			}
			//Invoke ("Stuck", 3f);
			//isStuck = true;
		}
	}

	//Detects if the weapon collides with another object. If it does, it checks who's turn it is and if the player
	//is aiming. After firing, it checks if it collides with the floor or the player. Then decides what to do from 
	//there.
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (GameManager.instance.p1Turn)
        {
			if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player") {
				if (shootScript.isAiming) {
					if (shootScript.move > 0 && !shootScript.maxRight) {
						maxRight = true;
					} else if (shootScript.move < 0 && !shootScript.maxLeft) {				
						maxLeft = true;
					}
				} else if (bounceCount == 0 && gameObject.tag == "Grenade") {
					bounceCount++;
				} else if (bounceCount > 0 || gameObject.tag == "Weapon") {
					//Destroy (this.gameObject, 0.5f);	
					//Debug.Log(playerScript.currHealth);
					if (gameObject.tag == "Grenade") {
						isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1f, blockingLayer);
						isGroundedWater = Physics2D.OverlapCircle(groundCheck.position, 1f, waterLayer);
						isGroundedRock = Physics2D.OverlapCircle(groundCheck.position, 1f, rockLayer);
						isGroundedLava = Physics2D.OverlapCircle(groundCheck.position, 1f, lavaLayer);
						if(isGrounded)
						{
							Invoke ("Explode", 0.1f);	
						}
						else if(isGroundedWater)
						{
							Invoke ("Explode", 0.1f);	
						}
						else if(isGroundedRock)
						{
							Invoke ("Explode", 0.1f);	
						}
						else if(isGroundedLava)
						{
							Invoke ("Explode", 0.1f);	
						}
					}
				 		
				}						
			}
		} 
		else if (GameManager.instance.p2Turn)
		{
			if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player") {
				if (shootScript.isAiming) {
					if (shootScript.move > 0 && !shootScript.maxRight) {
						maxRight = true;
					} else if (shootScript.move < 0 && !shootScript.maxLeft) {				
						maxLeft = true;
					}
				} else if (bounceCount == 0 && gameObject.tag == "Grenade") {
					bounceCount++;
				} else if (bounceCount > 0 || gameObject.tag == "Weapon") {
					//Destroy (this.gameObject, 0.5f);	
					//Debug.Log(playerScript.currHealth);
					if (gameObject.tag == "Grenade") {
						isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1f, blockingLayer);
						isGroundedWater = Physics2D.OverlapCircle(groundCheck.position, 1f, waterLayer);
						isGroundedRock = Physics2D.OverlapCircle(groundCheck.position, 1f, rockLayer);
						isGroundedLava = Physics2D.OverlapCircle(groundCheck.position, 1f, lavaLayer);
						if(isGrounded)
						{
							Invoke ("Explode", 0.1f);	
						}
						else if(isGroundedWater)
						{
							Invoke ("Explode", 0.1f);	
						}
						else if(isGroundedRock)
						{
							Invoke ("Explode", 0.1f);	
						}
						else if(isGroundedLava)
						{
							Invoke ("Explode", 0.1f);	
						}
					}

				}						
			}
		} 
	}

	//On collision exit. 
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player")
        {
			if (shootScript.isAiming) {
				
					maxRight = false;
				
					maxLeft = false;
			}
		}
	}

	private void Explode()
	{
		hitsPlayer = Physics2D.OverlapCircle (gameObject.transform.position, 10f, playerLayer);
		hitsGround = Physics2D.OverlapCircle (gameObject.transform.position, 10f, blockingLayer);
		gameObject.GetComponent<Rigidbody2D> ().rotation = 0.0f;
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;   
		anim.SetTrigger ("Bounce");

		if (hitsPlayer) {
			if (GameManager.instance.p1Turn) {
				playerScript2.currHealth -= 5;
			}
			else if (GameManager.instance.p2Turn) {
				playerScript.currHealth -= 5;
				}
		}
		if (hitsGround && canDestroy) {			
			Invoke ("DestroyTile", 1f);
		}
		Destroy (gameObject, 1.5f);
		//if (isStuck) {

		//} else {
		SwitchTurns();
		//}
	}

	private void Stuck()
	{
		if (oldPos == Vector3.zero) {
			oldPos = gameObject.transform.position;
		} else {
			Invoke("Explode", 0.5f);
		}

	}

	private void DestroyTile()
	{
		Vector3Int gridPos = grid.WorldToCell (gameObject.transform.position);			                   
		Vector3Int tilePosDown = new Vector3Int (gridPos.x, gridPos.y - 1, 0);
		Vector3Int tilePosUp = new Vector3Int (gridPos.x, gridPos.y + 1, 0);
		Vector3Int tilePosLeft = new Vector3Int (gridPos.x - 1, gridPos.y, 0);
		Vector3Int tilePosRight = new Vector3Int (gridPos.x + 1, gridPos.y, 0);
		Vector3Int tilePosUpRight = new Vector3Int (gridPos.x + 1, gridPos.y + 1, 0);
		Vector3Int tilePosUpLeft = new Vector3Int (gridPos.x - 1, gridPos.y + 1, 0);
		Vector3Int tilePosDownRight = new Vector3Int (gridPos.x + 1, gridPos.y - 1, 0);
		Vector3Int tilePosDownLeft = new Vector3Int (gridPos.x - 1, gridPos.y - 1, 0);
		tilemap.SetTile (tilePosDown, null);
		tilemap.SetTile (tilePosUp, null);
		tilemap.SetTile (tilePosLeft, null);
		tilemap.SetTile (tilePosRight, null);
		tilemap.SetTile (tilePosUpRight, null);
		tilemap.SetTile (tilePosUpLeft, null);
		tilemap.SetTile (tilePosDownRight, null);
		tilemap.SetTile (tilePosDownLeft, null);
	}

	private void SwitchTurns()
	{
		Debug.Log ("turn is:");
		Debug.Log (GameManager.instance.p1Turn);
		Debug.Log ("other turn is:");
		Debug.Log (GameManager.instance.p2Turn);
		shootScript.currentWeapon = null;
		shootScript.isThrown = false;
		if (GameManager.instance.p1Turn ) {
			GameManager.instance.p1Turn = false;
			GameManager.instance.p2Turn = true;
			playerScript2.movementCount = 0;
		} else if (GameManager.instance.p2Turn) {
			GameManager.instance.p1Turn = true;
			GameManager.instance.p2Turn = false;
			playerScript.movementCount = 0;
		}
	}

}
