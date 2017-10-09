using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector2 target;
	private Vector3 position;
	public GameObject tower;
    public int moving;
	public int money;
    public static string mode = "Slashy";

	void Start () {
		moving = 0;
		money = 100;
	}

	void Update () {
		if (Input.GetKeyDown ("e")) {
			mode = "Build";
		}

        if (moving == 1)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(target);
			this.transform.position = Vector2.MoveTowards(this.transform.position, pos, 4.0f * Time.deltaTime);
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
}
