using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moveLeft : Button
{

	private Player1 leftp1;
	private Player2 leftp2;
   private Button rightButton;
    public bool isClicked;



    void Start()
    {
        leftp1 = GameObject.Find("Player1").GetComponent<Player1>();
        leftp2 = GameObject.Find("Player2").GetComponent<Player2>();
        rightButton = GameObject.Find("Right").GetComponent<moveRight>();
    }

    private void Update()
		{
			//A public function in the selectable class which button inherits from.
			if(IsPressed())
			{
          

            LeftWhilePressed();
			}
		else if(!IsPressed() )
		{
            leftp1.move = 0;
            leftp2.move = 0;
		}
		}

    private void LeftWhilePressed()
		{
			if(GameManager.instance.p1Turn)
			{
            leftp1.move = -1;
			}
			else if(GameManager.instance.p2Turn)
			{
            leftp2.move = -1;
			}
		}
}

