using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moveRight : Button
{
	private Player1 p1;
	private Player2 p2;
    private Button leftButton;
    public bool isClicked;

    
    void Start()
    {
        p1 = GameObject.Find("Player1").GetComponent<Player1>();
        p2 = GameObject.Find("Player2").GetComponent<Player2>();
        leftButton = GameObject.Find("Left").GetComponent<moveLeft>();

    }

    private void Update()
	{
		//A public function in the selectable class which button inherits from.
		if(IsPressed())
		{
            // Debug.Log("clicked right");
            isClicked = true;


            WhilePressed();
		}
		else if(!IsPressed() && !leftButton.IsActive())
		{
            isClicked = false;
            p1.move = 0;
            p2.move = 0;

        }
	}

    private void WhilePressed()
	{
		if(GameManager.instance.p1Turn)
		{
			p1.move = 1;
		}
		else if(GameManager.instance.p2Turn)
		{
			p2.move = 1;
		}
	}
}

