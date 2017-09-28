using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static GameObject player;
    public static int money = 0;
    public Transform transform;
    private Vector2 target;
    public int moving = 0;
    public static GameObject tower;
    public static GameObject buildable;
    public static GameObject activeAbility;
    public static string mode = "Slashy";
	// Use this for initialization
	void Start () {
        buildable = GameObject.FindGameObjectWithTag("Buildable"); // temp
        tower = GameObject.FindGameObjectWithTag("Tower"); //temp
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {

        if (moving == 1)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(target);
            transform.position = Vector2.MoveTowards(transform.position, pos, 2.0f * Time.deltaTime);
        }
        if (mode == "Slashy"){

            if (Input.GetMouseButton(1))
            {
                moving = 1;
                target = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                
                //cast(activeAbility);
            }
        }
        else if (mode == "Build")
        {
            if (Input.GetMouseButton(0))
            {
                
            }

            else if (Input.GetMouseButton(1))
            {
                mode = "Slashy";
            }
        }
        
	}


    public static void placeTower()
    {
        tower.transform.position = (buildable.transform.position);
    }



    
}
