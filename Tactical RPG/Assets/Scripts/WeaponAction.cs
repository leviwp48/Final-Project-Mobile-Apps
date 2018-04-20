using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction : MonoBehaviour {

	[SerializeField]
	private LayerMask playerLayer;
	[SerializeField]
	private LayerMask blockingLayer;

    private Shoot shootScript;
	private Player1 playerScript;
	private Player2 playerScript2;
	private Animator anim;
	private int bounceCount;
	private bool hitsPlayer;
	private bool hitsGround;

	public bool maxRight;
	public bool maxLeft;

	void Awake()
	{
		if (gameObject.name == "Grenade") {
			bounceCount = 0;
		}

		anim = this.GetComponent<Animator>();
		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
	}

	void Update()
	{
		if (GameManager.instance.p1Turn) {
			playerScript = GameObject.Find ("Player1").GetComponent<Player1> ();
		}
		else if(GameManager.instance.p2Turn) {
			playerScript2 = GameObject.Find ("Player2").GetComponent<Player2> ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (GameManager.instance.p1Turn) {
			if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player") {
				Debug.Log (coll.gameObject.tag);
				if (shootScript.isAiming) {
					if (shootScript.move > 0 && !shootScript.maxRight) {
						maxRight = true;
					} else if (shootScript.move < 0 && !shootScript.maxLeft) {				
						maxLeft = true;
					}
				} else if (bounceCount == 0 && gameObject.name == "Grenade(Clone)") {
					bounceCount++;
				} else if (bounceCount > 0 || gameObject.name != "Grenade(Clone)") {
					//Destroy (this.gameObject, 0.5f);	
					//Debug.Log(playerScript.currHealth);
					hitsPlayer = Physics2D.OverlapCircle (gameObject.transform.position, 3.0f, playerLayer);
					hitsGround = Physics2D.OverlapCircle (gameObject.transform.position, 3.0f, blockingLayer);
					if (hitsPlayer) {
						Debug.Log(gameObject.name);
						if (gameObject.name == "Grenade(Clone)") {
							playerScript2.currHealth -= 5;
							anim.SetTrigger ("Bounce"); 
						} else if (gameObject.name == "Javelin") {
							playerScript2.currHealth -= 10;
						}
					} else if (hitsGround) {
						if (this.name == "Grenade") {
							anim.SetTrigger ("Bounce"); 
						} else if (this.name == "Javelin") {
							Debug.Log ("hit the ground");
						}
					}

					gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
					Destroy (this.gameObject, 1.5f);

					GameManager.instance.p1Turn = false;
					GameManager.instance.p2Turn = true;
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
				} else if (bounceCount == 0 && gameObject.name == "Grenade") {
					bounceCount++;
				} else if (bounceCount > 0 && gameObject.name == "Grenade") {
					//Destroy (this.gameObject, 0.5f);	
					//Debug.Log(playerScript.currHealth);
					hitsGround = Physics2D.OverlapCircle (this.transform.position, 3.0f, blockingLayer);
					hitsPlayer = Physics2D.OverlapCircle (this.transform.position, 3.0f, playerLayer);
					if (hitsPlayer) {
						Debug.Log(gameObject.name);
						if (gameObject.name == "Grenade") {
							playerScript2.currHealth -= 5;
							anim.SetTrigger ("Bounce"); 
						} else if (gameObject.name == "Javelin") {
							playerScript2.currHealth -= 10;
						}
					} else if (hitsGround) {
						if (gameObject.name == "Grenade") {
							anim.SetTrigger ("Bounce"); 
						} else if (gameObject.name == "Javelin") {
							Debug.Log ("hit the ground");
						}
					}

					gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
					Destroy (this.gameObject, 1.5f);

					GameManager.instance.p1Turn = true;
					GameManager.instance.p2Turn = false;
				}
			}
		}
	}
		
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
}
