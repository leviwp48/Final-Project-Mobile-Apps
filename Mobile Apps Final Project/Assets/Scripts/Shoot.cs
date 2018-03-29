using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float launchSpeed;
    
    [SerializeField]
    private GameObject snowBall;
    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private LayerMask playerLayer;

    private Vector2 playerPos;
    private Vector2 spawnSnowBall;
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
        playerPos = new Vector2(playerPosition.position.x, playerPosition.position.y);
    }

   void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePos);
            Debug.Log("not working");
            spawnSnowBall = new Vector2(mousePos.x - playerPos.x - rb2d.position.x, mousePos.y - playerPos.y - rb2d.position.y);
            Debug.Log(spawnSnowBall);
            Debug.Log("not working");
            spawnSnowBall = new Vector2(Mathf.Abs(spawnSnowBall.x), Mathf.Abs(spawnSnowBall.y));
            Debug.Log(spawnSnowBall);

            //RaycastHit2D hit = Physics2D.Linecast(mousePos, playerPosition.transform.right, playerLayer);
            //if (hit.point != null)
            //{
            //Debug.Log(hit.point);
            //spawnSnowBall = hit.point;

            Debug.Log("This is object count");
                Debug.Log(objectCount);
                if (objectCount == 1)
                {
                    Destroy(previousBall);
                    objectCount = 0;
                }
                Launch();
                objectCount++;
            //}
            //else if (hit.collider == null)
            //{
            //    Debug.Log("was null");
            //}
        }
    }
    
    void Launch()
    {
        if(playerScript.isFacingLeft)
        {
            Vector2 left = new Vector2(-spawnSnowBall.x, spawnSnowBall.y);
            GameObject snowBallInstance =
                    Instantiate(snowBall, left, Quaternion.identity) as GameObject;           
            Rigidbody2D rb2dSnow = snowBallInstance.GetComponent<Rigidbody2D>();
            rb2dSnow.AddForce(-snowBallInstance.transform.right * launchSpeed);
            previousBall = snowBallInstance;
        }
        else
        {
            Vector2 right = new Vector2(spawnSnowBall.x, spawnSnowBall.y);
            GameObject snowBallInstance =
                    Instantiate(snowBall, right, Quaternion.identity) as GameObject;
            Rigidbody2D rb2dSnow = snowBallInstance.GetComponent<Rigidbody2D>();
            rb2dSnow.AddForce(snowBallInstance.transform.right * launchSpeed);
            previousBall = snowBallInstance;
        }
    }
}
