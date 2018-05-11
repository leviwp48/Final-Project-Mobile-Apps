using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private Text endText;

    public Player1 player1;
    public Player2 player2;
	public Rigidbody2D rbp1;
	public Rigidbody2D rbp2;
	public Shoot shootScript;
	public WeaponAction weaponScript;

    public static GameManager instance = null;
    public bool p1Turn;
    public bool p2Turn;
    public bool end;
	public Button skipButton;
	public Button canDestroyButton;

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
		weaponScript.canDestroy = false;

		canDestroyButton.onClick.AddListener (ChangeDestroy);
		skipButton.onClick.AddListener (SwitchTurns);
		endText = GameObject.Find("GameOverText").GetComponent<Text>();;

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(p1Turn)
		{
			rbp2.constraints = RigidbodyConstraints2D.FreezeAll;
			rbp1.constraints = RigidbodyConstraints2D.None;
			rbp1.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
		else if(p2Turn)
		{
			rbp1.constraints = RigidbodyConstraints2D.FreezeAll;
			rbp2.constraints = RigidbodyConstraints2D.None;
			rbp2.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		if (player1.currHealth <= 0) {
			end = true;
		}
		else if (player2.currHealth <= 0) {
			end = true;
		}

		if (end) {
			endText.enabled = true;
			Invoke("EndGame", 1.5f);
		}
	}

	private void EndGame()
	{
		SceneManager.LoadScene("Outro");
	}

private void SwitchTurns()
	{
		shootScript.isThrown = false;
		if (p1Turn) {
			p1Turn = false;
			p2Turn = true;
			player2.movementCount = 0;
			shootScript.currentWeapon = null;
		} else if (p2Turn) {
			p1Turn = true;
			p2Turn = false;
			player1.movementCount = 0;
			shootScript.currentWeapon = null;
		}
	}

	private void ChangeDestroy()
	{
		if (!weaponScript.canDestroy) {
			weaponScript.canDestroy = true;
		}
		else if (weaponScript.canDestroy) {
			weaponScript.canDestroy = false;
		}
	}
}
