using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModernAudioController : MonoBehaviour {
    public AudioSource[] sources;
    public static int activeflutes;
    public static int activedrums;
    public static int activepianos;
    public static int activeviolins;
    float time;
    float timeint;
	// Use this for initialization
	void Start () {
        activeflutes = 0;
        activedrums = 0;
        activeviolins = 0;
        activepianos = 0;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeint = Time.time - time;
        if (activeflutes > 0)
        {
            sources[0].mute = false;
        }
        if (activeviolins > 0)
        {
            sources[1].mute = false;
        }
        
        if (activepianos > 0)
        {
            sources[2].mute = false;
        }
        if (activedrums > 0)
        {
            sources[3].mute = false;
        }
        
	}

   

}
