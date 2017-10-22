using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioSource[] sources;
    public static int activedrums;
    public static int activeflutes;
    float time;
    float timeint;
    bool started;
	// Use this for initialization
	void Start () {
        started = false;
        activedrums = 0;
        activeflutes = 0;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeint = Time.time - time;
        if (timeint >= 12 || started == false)
        {
            if (activedrums == 1 && sources[0].isPlaying == false)
            {
                sources[0].Play();
                started = true;
                
            }
            if (activedrums >= 2 && sources[1].isPlaying == false)
            {

                sources[1].Play();    
                if (sources[0].isPlaying == false)
                {
                    sources[0].Play();
                }
                started = true;
            }
            if (activeflutes == 1 && sources[2].isPlaying == false)
            {
                sources[2].Play();
                started = true;
            }
            if (activeflutes >= 2 && sources[3].isPlaying == false)
            {
                sources[3].timeSamples = Mathf.RoundToInt(sources[3].clip.length* sources[3].clip.frequency*sources[3].clip.channels / 2) -2;
                sources[3].Play();
                if (sources[2].isPlaying == false)
                {
                    sources[2].Play();
                }
                started = true;
            }
            time = Time.time;
        }
	}

   

}
