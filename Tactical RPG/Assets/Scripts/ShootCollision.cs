using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCollision : MonoBehaviour {


	[SerializeField]
	private GameObject camera;
	[SerializeField]
	private GameObject player;

	private Rigidbody2D rb2d;
	private Shoot shootScript;
	private Player playerScript;
	private Animator anim;
	private int bounceCount;

	public bool maxRight;
	public bool maxLeft;

	void Awake()
	{
		//shootScript = camera.GetComponent<Shoot>();
		//playerScript = player.GetComponent<Player>();
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
		playerScript = GameObject.Find("Player").GetComponent<Player>();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor") 
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
			else if (bounceCount == 1)
			{
				//Destroy (this.gameObject, 0.5f);		
				anim.SetBool("bounce", true); 
				gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
				Destroy (this.gameObject, 1.5f);

			 }
		}

		if (coll.gameObject.tag == "Player2")
		{
			Destroy (this.gameObject, 0.5f);		
		}
	}


	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor") {
			if (shootScript.isAiming) {
				
					maxRight = false;
				
					maxLeft = false;
				
			}
		}
	}
}
