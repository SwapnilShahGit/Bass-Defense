using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicalAudioController : MonoBehaviour {
    public AudioSource[] sources;
    public static int activebags;
    public static int activeharps;
    public static int activetrumpets;
    public static int activepianos;
    public static int activeviolins;
    float time;
    float timeint;
    bool started;
	// Use this for initialization
	void Start () {
        started = false;
        activeharps = 0;
        activetrumpets = 0;
        activepianos = 0;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeint = Time.time - time;
        if (activebags > 0)
        {
            sources[0].mute = false;
        }
        if (activeharps > 0)
        {
            sources[1].mute = false;
        }
        if (activetrumpets == 1)
        {
            sources[2].mute = false;
        }
        if (activetrumpets >= 2)
        {
            sources[2].mute = false;
            sources[3].mute = false;
        }
        if (activepianos > 0)
        {
            sources[4].mute = false;
        }
        if (activeviolins > 0)
        {
            sources[5].mute = false;
        }
        
	}

   

}
