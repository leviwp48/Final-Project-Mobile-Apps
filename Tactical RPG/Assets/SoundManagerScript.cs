using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {


    public static AudioClip grenadeSound, dogSound, deathSound;
    static AudioSource audioSrc;

	// Use this for initialization
	void Start ()
    {
        grenadeSound = Resources.Load<AudioClip>("ka-BOOM");
        dogSound = Resources.Load<AudioClip>("NANI");
        deathSound = Resources.Load<AudioClip>("Noooo");

        audioSrc = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
