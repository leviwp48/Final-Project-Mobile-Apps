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

	public Player2 p2;
	private float oldHP;
	private Shoot shootScript;

	private WeaponAction weaponScript;
   
	void Awake()
	{
		shootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
		string[] temp = HPTextP2.text.Split(':');
		HPTextP2.text = temp[0] + ": " + p2.maxHealth + "/" + p2.maxHealth;

		fillAmount2 = Map(p2.maxHealth, 0, p2.maxHealth, 0, 1);

	}
    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		//oldHP = p2.currHealth;
		if(shootScript.isAiming)
		{
		weaponScript = GameObject.FindGameObjectWithTag("Grenade").GetComponent<WeaponAction>();
		}

		if(oldHP != p2.currHealth)
		{
			string[] temp = HPTextP2.text.Split(':');
			HPTextP2.text = temp[0] + ": " + p2.currHealth + "/" + p2.maxHealth;

			fillAmount2 = Map(p2.currHealth, 0, p2.maxHealth, 0, 1);
		}
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
