using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text HPTextP1;

    public float MaxValue { get; set; }

	public Player1 p1;
	private float oldHP;

	private WeaponAction weaponScript;
	private Shoot shootScript;

	void Awake()
	{
		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
		string[] temp = HPTextP1.text.Split(':');
		HPTextP1.text = temp[0] + ": " + p1.maxHealth + "/" + p1.maxHealth;

		fillAmount = Map(p1.maxHealth, 0, p1.maxHealth, 0, 1);
	
	}
    
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		//oldHP = p1.currHealth;
		if(shootScript.isAiming)
		{
		weaponScript = GameObject.FindGameObjectWithTag("Grenade").GetComponent<WeaponAction>();
		}

		if(oldHP != p1.currHealth)
		{
			string[] temp = HPTextP1.text.Split(':');
			HPTextP1.text = temp[0] + ": " + p1.currHealth + "/" + p1.maxHealth;

			fillAmount = Map(p1.currHealth, 0, p1.maxHealth, 0, 1);
		}

        HandleBar();
	}

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
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
