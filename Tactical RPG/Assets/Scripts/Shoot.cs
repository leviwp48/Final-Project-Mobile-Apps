using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public float rotateSpeed;
	public float launchSpeed;
	public bool isAiming;
	public float hitRadius;
	public bool maxRight;
	public bool maxLeft;
	public float move;

	[SerializeField]
	private GameObject snowBall;
	[SerializeField]
	private Transform snowBallTrans;
	[SerializeField]
	private Transform playerTrans;
	[SerializeField]
	private LayerMask playerLayer;
	[SerializeField]
	private LayerMask blockingLayer;

    private GameObject weaponSelect;
	private Vector3 spawnSnowBall;
	private GameObject previousBall;
	private Player playerScript;  
	private int objectCount;
	private Vector2 mousePos;
	private bool isWall;
	private bool isPlayer;
	private ShootGrenade grenadeScript;
    private ShootJavelin javelinScript;

    private Animator collAnim;

	void Start()
	{
		playerScript = GameObject.Find("Player").GetComponent<Player>();
		objectCount = 0;
		isAiming = false;

		//spawnSnowBall = new Vector3 (snowBallTrans.position.x, snowBallTrans.position.y, 0f);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0) && !isAiming)
		{		
			if (objectCount == 1) 
			{
				Destroy (previousBall);
				objectCount = 0;
			}
			Spawn ();
			objectCount++;

            //have something that grabs weapon select form manager


			//mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - previousBall.transform.position;
			//float angle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			//Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			//previousBall.transform.rotation = Quaternion.Slerp (previousBall.transform.rotation, rotation, rotateSpeed * Time.deltaTime);
			//RaycastHit2D hit = Physics2D.Linecast(mousePos, playerPosition.transform.right, playerLayer);
			//if (hit.point != null)
			//{
			//Debug.Log(hit.point);
			//spawnSnowBall = hit.point;


			//}
			//else if (hit.collider == null)
			//{
			//    Debug.Log("was null");
			//}
		} 
		else if(Input.GetMouseButtonDown (0) && isAiming)
		{
			launch();
		}
		else if (isAiming) 
		{     
            //for grenade weapon
            //grenadeScript = GameObject.FindGameObjectWithTag("Weapon1").GetComponent<ShootGrenade>();

            //for javelin weapon
            javelinScript = GameObject.FindGameObjectWithTag("Weapon2").GetComponent<ShootJavelin>();

            move = Input.GetAxis ("Horizontal");
			if (playerScript.isFacingLeft && move > 0 && !javelinScript.maxRight) 
			{
				previousBall.transform.RotateAround (playerTrans.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
			} 
			else if(playerScript.isFacingLeft && move < 0 && !javelinScript.maxLeft)
			{
				previousBall.transform.RotateAround (playerTrans.position, Vector3.forward, rotateSpeed * Time.deltaTime);
			}
			else if (!playerScript.isFacingLeft && move > 0 && !javelinScript.maxRight) 
			{
				previousBall.transform.RotateAround (playerTrans.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
			} 
			else if (!playerScript.isFacingLeft && move < 0 && !javelinScript.maxLeft) 				
			{
				previousBall.transform.RotateAround (playerTrans.position, Vector3.forward, rotateSpeed * Time.deltaTime);
			}
		}
	}
		
	private void Spawn()
	{
		if(playerScript.isFacingLeft)
		{
			//Vector3 offset = transform.position - playerTrans.position + snowBallTrans.position;
			// offset = new Vector2(offset.x, offset.y);
			//Debug.Log(offset);
			GameObject snowBallInstance =
				Instantiate(snowBall, snowBallTrans.position, Quaternion.identity) as GameObject;    
			Rigidbody2D rb2dSnow = snowBallInstance.GetComponent<Rigidbody2D>();
			//rb2dSnow.AddForce(-snowBallInstance.transform.right * launchSpeed);
			rb2dSnow.gravityScale = 0.0f;
			previousBall = snowBallInstance;
			isAiming = true;
		}
		else
		{
     		// Vector2 right = new Vector2(spawnSnowBall.x, spawnSnowBall.y);
			// Debug.Log(right);

			GameObject snowBallInstance =
				Instantiate(snowBall, snowBallTrans.position, Quaternion.identity) as GameObject;
			Rigidbody2D rb2dSnow = snowBallInstance.GetComponent<Rigidbody2D>();
			//rb2dSnow.AddForce(snowBallInstance.transform.right * launchSpeed);
			rb2dSnow.gravityScale = 0.0f;
			previousBall = snowBallInstance;
			isAiming = true;
		}
	}

	private void launch()
	{
		if (playerScript.isFacingLeft) {
			Rigidbody2D rb2dSnow = previousBall.GetComponent<Rigidbody2D> ();
			rb2dSnow.gravityScale = 1f;
			rb2dSnow.AddForce (-previousBall.transform.right * launchSpeed);
			isAiming = false;
		} 
		else {
			Rigidbody2D rb2dSnow = previousBall.GetComponent<Rigidbody2D> ();
			rb2dSnow.gravityScale = 1f;
			rb2dSnow.AddForce (previousBall.transform.right * launchSpeed);
			isAiming = false;
			}
		}

}
