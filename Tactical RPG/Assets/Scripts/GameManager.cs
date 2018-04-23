using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Player1 player1;
    private Player2 player2;

    public static GameManager instance = null;
    public bool p1Turn;
    public bool p2Turn;
    public bool end;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

		p1Turn = true;
		p2Turn = false;
		end = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
