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
    

	//May not be necessary. But helps in filtering what we contact
    public ContactFilter2D contactFilter;

	//Is True if the player is aiming and the weapon hits the ground
	public bool maxRight;
	public bool maxLeft;

	//On awake, the weapon will initialize the bouncecount to 0 if its a grenade.
	//It will set the tilemap, animator, shootScript, grid, and contact filter.
	void Awake()
	{
		if (gameObject.name == "Grenade") {
			bounceCount = 0;
		}

		tilemap = GameObject.Find("Map").GetComponent<Tilemap> ();
		anim = this.GetComponent<Animator>();
		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
		grid = GameObject.Find("Grid").GetComponent<Grid> ();
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(blockingLayer);
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
	}

	//Detects if the weapon collides with another object. If it does, it checks who's turn it is and if the player
	//is aiming. After firing, it checks if it collides with the floor or the player. Then decides what to do from 
	//there.
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (GameManager.instance.p1Turn)
        {
			if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player")
            {
				Debug.Log (coll.gameObject.tag);
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
						hitsPlayer = Physics2D.OverlapCircle (gameObject.transform.position, 10f, playerLayer);
						hitsGround = Physics2D.OverlapCircle (gameObject.transform.position, 10f, blockingLayer);
						Invoke ("Explode", 0.8f);							
					}
				 		

					Debug.Log ("player 2's turn");
					GameManager.instance.p1Turn = false;
					GameManager.instance.p2Turn = true;
					playerScript2.movementCount = 0;
				}
			}
		} 
		else if (GameManager.instance.p2Turn)
		{
			if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player")
			{
				Debug.Log (coll.gameObject.tag);
				if (shootScript.isAiming)
				{
					if (shootScript.move > 0 && !shootScript.maxRight)
					{
						maxRight = true;
					} else if (shootScript.move < 0 && !shootScript.maxLeft)
					{				
						maxLeft = true;
					}
				}
				else if (bounceCount == 0 && gameObject.tag == "Grenade")
				{
					bounceCount++;
				}
				else if (bounceCount > 0 || gameObject.tag == "Weapon")
				{
					//Destroy (this.gameObject, 0.5f);	
					//Debug.Log(playerScript.currHealth);
					if (gameObject.tag == "Grenade") {
						hitsPlayer = Physics2D.OverlapCircle (gameObject.transform.position, 10f, playerLayer);
						if (hitsPlayer) {
							Debug.Log (hitsPlayer);
							playerScript.currHealth -= 5;
							Invoke ("Explode", 1.5f);
						} else {
							hitsGround = Physics2D.OverlapCircle (gameObject.transform.position, 10f, blockingLayer);
							Debug.Log (hitsGround);

							if (hitsGround) {
								Invoke ("Explode", 1.5f);
							}
						}
					} else {
						hitsPlayer = Physics2D.OverlapCircle (gameObject.transform.position, 6f, playerLayer);
						if (hitsPlayer) {
							playerScript.currHealth -= 10;
						} else {
							hitsGround = Physics2D.OverlapCircle (gameObject.transform.position, 6f, blockingLayer);
							if (hitsGround) {
								Debug.Log ("hit the ground");                  
							}
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
		gameObject.GetComponent<Rigidbody2D> ().rotation = 0.0f;
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		anim.SetTrigger ("Bounce");   

		if (hitsPlayer) {
			Debug.Log (hitsPlayer);
			playerScript.currHealth -= 5;
		}
		if (hitsGround) {			
			Invoke ("DestroyTile", 1f);
		}

		Destroy (gameObject, 1.5f);
        shootScript.isThrown = false;
	}

	private void DestroyTile()
	{
		Vector3Int gridPos = grid.WorldToCell (gameObject.transform.position);			                   
		Vector3Int tilePos = new Vector3Int (gridPos.x, gridPos.y - 1, 0);
		tilemap.SetTile (tilePos, null);
	}
}
