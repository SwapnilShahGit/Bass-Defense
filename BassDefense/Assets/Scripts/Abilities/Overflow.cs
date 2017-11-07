using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overflow: MonoBehaviour {

    public AudioSource sfx;
    public static int onCD;
    static int cost;
    float dur;
    static TowerController[] towers;
    public static float time;
    public float timeint;
    public float cd;
    // Use this for initialization
    void Start()
    {
        onCD = 0;
        cost = 10;
        cd = 10;
        time = Time.time;
        timeint = 0;
    }
	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("2"))
        {
            cast();
        }

        timeint = Time.time - time;
        if (timeint > cd)
        {
            onCD = 0;
        }  
	}

    public void cast()
    {
        if (onCD == 0 && PlayerController.flow >= cost)
        {
            sfx.Play();
            GameObject b = Instantiate((GameObject)Resources.Load("Overflow"));
            b.transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            
            b.GetComponent<OverflowBehaviour>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            PlayerController.flow -= cost;
            onCD = 1;
            time = Time.time;
        }
        else
        {
            //on cooldown sound
        }
    }
    
}
