using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatPlayer2
{
    [SerializeField]
    private BarScriptPlayer2 bar2;

    [SerializeField]
    private float maxVal2;

    [SerializeField]
    private float currentVal2;

    public float CurrentVal2
    {
        get
        {
            return currentVal2;
        }

        set
        {
            //keeps the health between 0 and its maximum value so we can't go over the max or under 0
            this.currentVal2 = Mathf.Clamp(value, 0, MaxVal2);
            bar2.Value = currentVal2;
        }
    }

    public float MaxVal2
    {
        get
        {
            return maxVal2;
        }

        set
        {
            maxVal2 = value;
            bar2.MaxValue2 = MaxVal2;
        }
    }

    public void Initialize()
    {
        this.MaxVal2 = maxVal2;
        this.CurrentVal2 = currentVal2;
    }
}
