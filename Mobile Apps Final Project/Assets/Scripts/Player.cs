using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject SnowBall;

    public float moveTime = 0.1f;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private bool isMoving;

    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();

        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject snowBallInstance =
                Instantiate(SnowBall, player.transform.position, Quaternion.identity) as GameObject;

            snowBallInstance.transform.position = new Vector2(transform.position.x + 5, transform.position.y + 2);
        }

        if (isMoving == false)
        {
            if (Input.GetKeyDown("d"))
            {
                float xPos = transform.position.x + 1.5f;
                float yPos = transform.position.y;
                Vector3 end = new Vector3(xPos, yPos, 0.0f);
                isMoving = true;
                StartCoroutine(SmoothMovement(end));
            }
            else if (Input.GetKeyDown("a"))
            {
                float xPos = transform.position.x - 1.5f;
                float yPos = transform.position.y;
                Vector3 end = new Vector3(xPos, yPos, 0.0f);
                isMoving = true;
                StartCoroutine(SmoothMovement(end));
            }
            else if (Input.GetKeyDown("space"))
            {
                float xPos = transform.position.x;
                float yPos = transform.position.y + 1.5f;
                Vector3 end = new Vector3(xPos, yPos, 0.0f);
                isMoving = true;
                StartCoroutine(SmoothMovement(end));
            }
        }
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            player.transform.position = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        isMoving = false;
    }
}
