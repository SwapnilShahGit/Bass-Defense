using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overflow: MonoBehaviour {
    public Texture2D ocursor;
    public Texture2D cursor;
    public AudioSource sfx;
    public static int onCD;
    static int cost;
    float dur;
    static TowerController[] towers;
    public static float time;
    public float timeint;
    public float cd;
    public bool isCasting;
    // Use this for initialization
    void Start()
    {
        isCasting = false;
        onCD = 0;
        cost = 10;
        cd = 10;
        time = Time.time;
        timeint = 0;
    }
	
	
	// Update is called once per frame
	void Update () {
        if (isCasting)
        {
            Cursor.SetCursor(ocursor, Vector2.zero, CursorMode.Auto);
        }
        if (Input.GetKeyDown("2") || (isCasting && (Input.GetMouseButton(0) || Input.GetMouseButton(1))) )
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
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            isCasting = false;
        }
        else
        {
            //on cooldown sound
        }
    }
    public void overflowbutton()
    {
        PlayerController.moving = 0;
        if (PlayerController.flow >= cost && onCD == 0)
        {
            isCasting = true;
        }
    }
    
}
