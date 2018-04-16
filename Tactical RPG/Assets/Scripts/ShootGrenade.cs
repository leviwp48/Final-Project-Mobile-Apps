using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGrenade : MonoBehaviour {

	[SerializeField]
	private GameObject player;
	[SerializeField]
	private LayerMask playerLayer;

    private Shoot shootScript;
	private Player1 playerScript;
	private Animator anim;
	private int bounceCount;
	private bool hitsPlayer;

	public bool maxRight;
	public bool maxLeft;

	void Awake()
	{
		bounceCount = 0;
		anim = GetComponent<Animator>();
		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
		playerScript = GameObject.Find("Player").GetComponent<Player1>();
		Debug.Log(playerScript.currHealth);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player2") 
		{
			if (shootScript.isAiming) {
				if (shootScript.move > 0 && !shootScript.maxRight) {
					maxRight = true;
				} else if (shootScript.move < 0 && !shootScript.maxLeft) {				
					maxLeft = true;
				}
			} else if (bounceCount == 0)
			{
				bounceCount++;
			}
			else if (bounceCount > 0)
			{
				//Destroy (this.gameObject, 0.5f);	
				//Debug.Log(playerScript.currHealth);
				hitsPlayer = Physics2D.OverlapCircle (gameObject.transform.position, 3.0f, playerLayer);
				if (hitsPlayer) {
					playerScript.currHealth -= 5;
					Debug.Log (playerScript.currHealth);
				}
				anim.SetTrigger("Bounce"); 
				gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
				Destroy (this.gameObject, 1.5f);

                if (GameManager.instance.p1Turn == true)
                {
                    GameManager.instance.p1Turn = false;
                    GameManager.instance.p2Turn = true;
                }
            }
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor")
        {
			if (shootScript.isAiming) {
				
					maxRight = false;
				
					maxLeft = false;
			}
		}
	}
}
