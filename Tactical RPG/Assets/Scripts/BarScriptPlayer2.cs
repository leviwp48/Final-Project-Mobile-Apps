using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScriptPlayer2 : MonoBehaviour
{
    private float fillAmount2;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content2;

    [SerializeField]
    private Text HPTextP2;

    public float MaxValue2 { get; set; }

    public float Value
    {
        set
        {
            //dynamic text for the health so that it will show current health/maxhealth(like 40/50)
            string[] temp2 = HPTextP2.text.Split(':');
            HPTextP2.text = temp2[0] + ": " + value + "/" + MaxValue2;

            fillAmount2 = Map(value, 0, MaxValue2, 0, 1);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleBar2();
    }

    private void HandleBar2()
    {
        if (fillAmount2 != content2.fillAmount)
        {
            content2.fillAmount = Mathf.Lerp(content2.fillAmount, fillAmount2, Time.deltaTime * lerpSpeed);
        }

    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        // this function sets the health bar's fill amount to our current health
        // by calculating the percentage of our health bar that should still be filled
        // value is our current health, inMin is our lowest possible health(0)
        // inMax is our highest possible health(50), outMin is our lowest possible
        // fill amount for our health(0), and outMax is our highest possible fill amount(1)
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

}
