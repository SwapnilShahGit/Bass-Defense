using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public static float starttime;
    float loopend;
    public static AudioSource master = null;
    public static AudioSource[] slaves = new AudioSource[3];
    public static int ssize = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (master!= null){
            if (master.isPlaying == false)
            {
                master.Play();
            }
            foreach (AudioSource s in slaves)
            {
                if (s.isPlaying == false && master.timeSamples == 0)
                {
                    s.Play();
                
                }
            }
                
           
        }
        SyncTracks();
	}

    private IEnumerator SyncTracks()
    {
      foreach (AudioSource slave in slaves)
            {
                slave.timeSamples = master.timeSamples;
                yield return null;
            }
      
    } 



    public static bool isQueued(AudioSource s)
    {
        if (master = s)
        {
            return true;
        }
        foreach (AudioSource slave in slaves)
        {
            if (slave == s)
            {
                return true;
            }
        }
        return false;
    }


    public static void queue(AudioSource s)
    {
        if (master == null)
        {
            master = s;
            master.Play();
        }
        else
        {
            slaves[ssize] = s;
            ssize++;
        }
    }
}
