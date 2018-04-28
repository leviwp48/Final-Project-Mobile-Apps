using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moveLeft2 : Button
{
    private Player1 p1;
    private Player2 p2;

    protected override void Start()
    {
        p1 = GameObject.Find("Player1").GetComponent<Player1>();
        p2 = GameObject.Find("Player2").GetComponent<Player2>();
    }

    public void Update()
    {
        //A public function in the selectable class which button inherits from.
        if(IsPressed())
        {
            WhilePressed();
        }
        else if(!IsPressed())
        {
            p1.move = 0;
            p2.move = 0;
        }
    }

    private void WhilePressed()
    {
        if (GameManager.instance.p1Turn)
        {
            p1.move = -1;
        }
        else if (GameManager.instance.p2Turn)
        {
            p2.move = -1;
        }
    }
}

