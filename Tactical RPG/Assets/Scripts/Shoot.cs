 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;


public class Shoot : MonoBehaviour {

	public float rotateSpeed;
	public float launchSpeed;
	public float[] gravity;
	public bool isAiming;
	public float hitRadius;
	public bool maxRight;
	public bool maxLeft;
	//will be true if an object has been thrown
	public bool isThrown;
	[HideInInspector]
	public float move;

	[SerializeField]
	private GameObject[] weaponSelect;
	[SerializeField]
	private Transform weaponTrans;
    [SerializeField]
    private Transform weaponTrans2;
    [SerializeField]
	private Transform playerTrans;
    [SerializeField]
    private Transform playerTrans2;
    [SerializeField]
	private LayerMask playerLayer;
	[SerializeField]
	private LayerMask blockingLayer;
    [SerializeField]
    private Button weaponButton;
	[SerializeField]
	private Button weaponButton2;
    [SerializeField]
    private Button weaponButton3;
    [SerializeField]
    private Button weaponButton4;
	[SerializeField]
	private Button rightButton;
	[SerializeField]
	private Button leftButton;
	[SerializeField]
	private Button jumpButton;


	[HideInInspector]
	public GameObject currentWeapon; 
	private Vector3 spawnSnowBall; 
	private GameObject previousWeapon;
	private Player1 playerScript; 
	private Player2 playerScript2;
	private int objectCount;
	private Vector2 mousePos;
	private bool isWall;
	private bool isPlayer;
	private WeaponAction weaponScript;
    private Animator collAnim;
 


    void Start()
	{
		objectCount = 0;
		isAiming = false;
	}

	void Update()
	{

		if (GameManager.instance.p1Turn) {
			playerScript = GameObject.Find ("Player1").GetComponent<Player1> ();
		}
		else if(GameManager.instance.p2Turn) {
			playerScript2 = GameObject.Find ("Player2").GetComponent<Player2> ();
		}
			
		weaponButton.onClick.AddListener (delegate{ChooseWeapon(weaponButton);});
		weaponButton2.onClick.AddListener (delegate{ChooseWeapon(weaponButton2);});
		weaponButton3.onClick.AddListener (delegate{ChooseWeapon(weaponButton3);});
		weaponButton4.onClick.AddListener (delegate{ChooseWeapon(weaponButton4);});

        // For Final Presentation
        // This restricts the players touches to only spawn and throw the grenade to the top half
        // of the users screen, so it will persist on any device
		if(Input.mousePosition.y > Screen.height / 2)
		{
			if (Input.GetMouseButtonDown (0) && !isAiming && !isThrown)
			{		
								
				if (objectCount == 1) 
				{
					Destroy (previousWeapon);
					objectCount = 0;
				}

				if (!isThrown) 
				{
					Spawn ();
					objectCount++;
				}
			} 
			else if(Input.GetMouseButtonDown (0) && isAiming)
			{
				launch();
			}
		}

		if (isAiming) 
		{     
			weaponScript = GameObject.FindGameObjectWithTag(currentWeapon.tag).GetComponent<WeaponAction>();

			move = 0;
			move = CrossPlatformInputManager.GetAxis("Horizontal");

			if (GameManager.instance.p1Turn) {
				if (playerScript.isFacingLeft && move > 0 && !weaponScript.maxRight) {
					previousWeapon.transform.RotateAround (playerTrans.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
				} else if (playerScript.isFacingLeft && move < 0 && !weaponScript.maxLeft) {
					previousWeapon.transform.RotateAround (playerTrans.position, Vector3.forward, rotateSpeed * Time.deltaTime);
				} else if (!playerScript.isFacingLeft && move > 0 && !weaponScript.maxRight) {
					previousWeapon.transform.RotateAround (playerTrans.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
				} else if (!playerScript.isFacingLeft && move < 0 && !weaponScript.maxLeft) {
					previousWeapon.transform.RotateAround (playerTrans.position, Vector3.forward, rotateSpeed * Time.deltaTime);
				}
			} else if (GameManager.instance.p2Turn) {
				if (playerScript2.isFacingLeft && move > 0 && !weaponScript.maxRight) {
					previousWeapon.transform.RotateAround (playerTrans2.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
				} else if (playerScript2.isFacingLeft && move < 0 && !weaponScript.maxLeft) {
					previousWeapon.transform.RotateAround (playerTrans2.position, Vector3.forward, rotateSpeed * Time.deltaTime);
				} else if (!playerScript2.isFacingLeft && move > 0 && !weaponScript.maxRight) {
					previousWeapon.transform.RotateAround (playerTrans2.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
				} else if (!playerScript2.isFacingLeft && move < 0 && !weaponScript.maxLeft) {
					previousWeapon.transform.RotateAround (playerTrans2.position, Vector3.forward, rotateSpeed * Time.deltaTime);
				}
			}
		
		}
	}
		
	private void Spawn()
	{
		if (GameManager.instance.p1Turn)
		{
			if (playerScript.isFacingLeft) 
			{			
				if (currentWeapon != null) {
					GameObject weaponInstance =
						Instantiate (currentWeapon, weaponTrans.position, Quaternion.identity) as GameObject; 
			
					Rigidbody2D rb2dSnow = weaponInstance.GetComponent<Rigidbody2D> ();
					rb2dSnow.gravityScale = 0.0f;
					previousWeapon = weaponInstance;
					isAiming = true;
				}
			} 
			else if(!playerScript.isFacingLeft)
			{
				
				if (currentWeapon != null) {
					GameObject weaponInstance =
						Instantiate (currentWeapon, weaponTrans.position, Quaternion.identity) as GameObject;
					Rigidbody2D rb2dSnow = weaponInstance.GetComponent<Rigidbody2D> ();
					rb2dSnow.gravityScale = 0.0f;
					previousWeapon = weaponInstance;
					isAiming = true;
				}
			}
		} 
		else if (GameManager.instance.p2Turn)
		{
			if (playerScript2.isFacingLeft)
			{
				if (currentWeapon != null) 
				{
					GameObject weaponInstance =
					    Instantiate (currentWeapon, weaponTrans2.position, Quaternion.identity) as GameObject; 
					Rigidbody2D rb2dSnow = weaponInstance.GetComponent<Rigidbody2D> ();
					rb2dSnow.gravityScale = 0.0f;
					previousWeapon = weaponInstance;
					isAiming = true;
				}
				else if(!playerScript2.isFacingLeft)
				{
					if (currentWeapon != null) {
						GameObject weaponInstance =
							Instantiate (currentWeapon, weaponTrans2.position, Quaternion.identity) as GameObject;
						Rigidbody2D rb2dSnow = weaponInstance.GetComponent<Rigidbody2D> ();
						rb2dSnow.gravityScale = 0.0f;
						previousWeapon = weaponInstance;
						isAiming = true;
					}
				}
			}
		}
	}

	private void launch ()
    {
        isThrown = true;
		if (GameManager.instance.p1Turn) {
			if (playerScript.isFacingLeft) {
				Rigidbody2D rb2dSnow = previousWeapon.GetComponent<Rigidbody2D> ();
				if (rb2dSnow.tag == "Grenade") {
					rb2dSnow.gravityScale = gravity[0];
				}
				else
				{
					rb2dSnow.gravityScale = gravity[1];
				}
				rb2dSnow.AddForce (-previousWeapon.transform.right * launchSpeed);
				isAiming = false;
			} else {
				Rigidbody2D rb2dSnow = previousWeapon.GetComponent<Rigidbody2D> ();
				if (rb2dSnow.tag == "Grenade") {
					rb2dSnow.gravityScale = gravity[0];
				}
				else
				{
					rb2dSnow.gravityScale = gravity[1];
				}
				rb2dSnow.AddForce (previousWeapon.transform.right * launchSpeed);
				isAiming = false;
			}
		} else if (GameManager.instance.p2Turn) {
			if (playerScript2.isFacingLeft) {
				Rigidbody2D rb2dSnow = previousWeapon.GetComponent<Rigidbody2D> ();
				if (rb2dSnow.tag == "Grenade") {
					rb2dSnow.gravityScale = gravity[0];
				}
				else
				{
					rb2dSnow.gravityScale = gravity[1];
				}
				rb2dSnow.AddForce (-previousWeapon.transform.right * launchSpeed);
				isAiming = false;
				} 
			else
			{
				Rigidbody2D rb2dSnow = previousWeapon.GetComponent<Rigidbody2D> ();
				if (rb2dSnow.tag == "Grenade") {
					rb2dSnow.gravityScale = gravity[0];
				}
				else
				{
					rb2dSnow.gravityScale = gravity[1];
				}
				rb2dSnow.AddForce (previousWeapon.transform.right * launchSpeed);
				isAiming = false;
			}
		}
	}
		

	private void ChooseWeapon(Button clickedButton)
	{
		if (clickedButton.name == "GrenadeButton") {
			currentWeapon = weaponSelect [0];	
		} else if (clickedButton.name == "NinjaDogButton") {
			currentWeapon = weaponSelect [1];
		}
	}

}
