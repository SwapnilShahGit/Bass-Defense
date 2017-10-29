using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicalAudioController : MonoBehaviour {
    public AudioSource[] sources;
    public static int activebags;
    public static int activeharps;
    public static int activetrumpets;
    public static int activepianos;
    float time;
    float timeint;
    bool started;
	// Use this for initialization
	void Start () {
        started = false;
        activebags = 0;
        activeharps = 0;
        activetrumpets = 0;
        activepianos = 0;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeint = Time.time - time;
        
        
	}

   

}
