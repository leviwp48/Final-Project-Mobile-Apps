using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCollision : MonoBehaviour {

	[SerializeField]
	private GameObject currBall;

	private Shoot shootScript;
	private Player playerScript;

	void Start()
	{
		shootScript = GameObject.Find("MainCamera").GetComponent<Shoot>();
		playerScript = GameObject.Find("Player1").GetComponent<Player>();

	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (shootScript.isAiming == true) 
		{
			shootScript.maxLeft = true;
			shootScript.maxRight = true;
		} 
		else 
		{
			if (coll.gameObject.tag == "Floor") 
			{
				Destroy (this.gameObject, 0.5f);		
			}
			if (coll.gameObject.tag == "Player2")
			{
				Destroy (this.gameObject, 0.5f);		
			}
		}
	}
}
