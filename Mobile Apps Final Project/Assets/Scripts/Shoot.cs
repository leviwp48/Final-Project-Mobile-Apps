using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float rotateSpeed;
    
    [SerializeField]
    private GameObject snowBall;
    [SerializeField]
    private Transform snowBallTrans;
    [SerializeField]
    private LayerMask playerLayer;

	private Vector3 spawnSnowBall;
    private GameObject previousBall;
    private Player playerScript;  
    private Rigidbody2D rb2d;
    private int objectCount;
    private Vector2 mousePos;
    
   void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        objectCount = 0;
		spawnSnowBall = new Vector3 (snowBallTrans.position.x, snowBallTrans.position.y, 0f);
    }

   void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			Debug.Log("This is object count");
			Debug.Log(objectCount);
			if (objectCount == 1)
			{
				Destroy(previousBall);
				objectCount = 0;
			}
			Spawn();
			objectCount++;

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
    }
    
    private void Spawn()
    {
        if(playerScript.isFacingLeft)
        {
            Vector2 left = new Vector2(-spawnSnowBall.x, spawnSnowBall.y);
            GameObject snowBallInstance =
                    Instantiate(snowBall, left, Quaternion.identity) as GameObject;    
            Rigidbody2D rb2dSnow = snowBallInstance.GetComponent<Rigidbody2D>();
            //rb2dSnow.AddForce(-snowBallInstance.transform.right * launchSpeed);
			rb2dSnow.gravityScale = 0.0f;
            previousBall = snowBallInstance;

        }
        else
        {
            Vector2 right = new Vector2(spawnSnowBall.x, spawnSnowBall.y);
            GameObject snowBallInstance =
                    Instantiate(snowBall, right, Quaternion.identity) as GameObject;
            Rigidbody2D rb2dSnow = snowBallInstance.GetComponent<Rigidbody2D>();
            //rb2dSnow.AddForce(snowBallInstance.transform.right * launchSpeed);
			rb2dSnow.gravityScale = 0.0f;

            previousBall = snowBallInstance;
        }
    }
}
