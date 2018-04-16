using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootJavelin : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private LayerMask playerLayer;

    private Shoot shootScript;
    private Player playerScript;
    private Animator anim;
    private bool hitsPlayer;

    public bool maxRight;
    public bool maxLeft;

    void Awake()
    {
        anim = GetComponent<Animator>();
        shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log(playerScript.currHealth);
    }

    void FixedUpdate()
    {
        hitsPlayer = Physics2D.OverlapCircle(gameObject.transform.position, 3.0f, playerLayer);
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player2")
        {
            if (shootScript.isAiming)
            {
                if (shootScript.move > 0 && !shootScript.maxRight)
                {
                    maxRight = true;
                }
                else if (shootScript.move < 0 && !shootScript.maxLeft)
                {
                    maxLeft = true;
                }
            }
            else if (hitsPlayer)
            {         
                playerScript.currHealth -= 10;
                Debug.Log(playerScript.currHealth);
            
                //anim.SetTrigger("Impale");
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                Destroy(this.gameObject, 1.5f);
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            if (shootScript.isAiming)
            {

                maxRight = false;

                maxLeft = false;

            }
        }
    }
}
