//------------------------------------------------------------------------------
// Author: Tyler Roop
// Date: 2/13/2017
// Credit: Unity Forums - Kaloss and Renanss Audio Manager: http://answers.unity3d.com/questions/154104/play-a-list-of-tracks-in-random-order.html
//
// Purpose: The purpose of this script is to help regulate the music
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    public AudioClip[] soundtrack;

    private AudioSource audioClip;

    [SerializeField] Pause pauseFunction;

    [SerializeField] Movement_Thomas_001 disable;

	// Use this for initialization
	void Start () {

        audioClip = GetComponent<AudioSource>();

        //pauseFunction = pauseFunction.GetComponent<Pause>();

        if (!audioClip.playOnAwake)
        {
            audioClip.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioClip.Play();
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        UpdateAudio();
	}

    public void UpdateAudio()
    {
        if (!pauseFunction.Paused)
        {
            audioClip.mute = false;

            if (!audioClip.isPlaying)
            {
                audioClip.clip = soundtrack[Random.Range(0, soundtrack.Length)];
                audioClip.Play();
               
            }
        }

        if (pauseFunction.Paused)
        {
            audioClip.mute = true;
        }

        if (disable.Disabled)
        {
            audioClip.Stop();
        }


    }
}
