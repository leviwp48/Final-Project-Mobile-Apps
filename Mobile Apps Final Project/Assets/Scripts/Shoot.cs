using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    [SerializeField]
    private GameObject snowBall;
    [SerializeField]
    private Transform spawnSnowBall;
    
    private Rigidbody2D rb2d;

   void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

   void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject snowBallInstance =
                    Instantiate(snowBall, spawnSnowBall.position, Quaternion.identity) as GameObject;


        }
    }
}
