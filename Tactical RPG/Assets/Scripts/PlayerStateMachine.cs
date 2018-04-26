using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    public Player1 player;
    public Player2 player2;

    public enum TurnState
    {
        PROCESSING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //for the progress(health) bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image HealthBar;

    //initialization
    void Start()
    {
        currentState = TurnState.PROCESSING;
    }

    //update once per frame
    void Update()
    {
        Debug.Log(currentState);
        switch(currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;

            case (TurnState.SELECTING):

                break;

            case (TurnState.ACTION):

                break;

            case (TurnState.DEAD):

                break;
        }
    }

    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        HealthBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
        if(cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.SELECTING;
        }
    }
}
