using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCollision : MonoBehaviour {

	[SerializeField]
	private GameObject currBall;

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor") {
			Destroy (this.gameObject, 0.5f);		
		}
		if (coll.gameObject.tag == "Player2") {
			Destroy (this.gameObject, 0.5f);		

		}
	}
}
