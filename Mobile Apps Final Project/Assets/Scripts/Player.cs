using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject SnowBall; 

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject snowBallInstance =
                Instantiate(SnowBall, player.transform.position, Quaternion.identity) as GameObject;

            snowBallInstance.transform.position = new Vector2(transform.position.x + 5, transform.position.y + 2);
        }

        if (Input.GetKeyDown("d"))
        {
            double currentPos = transform.position.x;
            currentPos = currentPos + 1;
            player.transform.position = new Vector2((float)currentPos, transform.position.y);
        }
        else if (Input.GetKeyDown("a"))
        {
            double currentPos = transform.position.x;
            currentPos = currentPos - 1;
            player.transform.position = new Vector2((float)currentPos, transform.position.y);
        }
        else if(Input.GetKeyDown("space"))
        {
            double currentPos = transform.position.y;
            currentPos = currentPos + 2;
            player.transform.position = new Vector2(transform.position.x, (float)currentPos);
        }
    }
}
