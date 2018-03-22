using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    [SerializeField]
    private GameObject SnowBall;
    [SerializeField]
    private GameObject player;

    private void OnMouseDown()
    {
        GameObject snowBallInstance =
                Instantiate(SnowBall, player.transform.position, Quaternion.identity) as GameObject;

        snowBallInstance.transform.position = new Vector2(transform.position.x + 5, transform.position.y + 2);
    }
}
