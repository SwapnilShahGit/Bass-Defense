using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    // UI tingz
    public Text playerHealthText;
    public Text playerMoneyText;


    public static GameObject player;
    public static Vector2 target;

    
	private Vector3 position;
	public static GameObject tower;
    public static int moving;
	public static int money;
    public static int health;
    public static string mode = "Slashy";

	void Start () {
		moving = 0;
		money = 1;
        health = 99;
        playerHealthText = GameObject.Find("PlayerHealth").GetComponent<Text>();
        playerMoneyText = GameObject.Find("Money").GetComponent<Text>();
        playerHealthText.text = "Health: " + health;
        playerMoneyText.text = "Money: " + money;
    }

    void Update () {
		if (Input.GetKeyDown ("e")) {
			mode = "Build";
            tower = (GameObject)Resources.Load("Drum");
           
		}
        if (Input.GetKeyDown("q"))
        {
            mode = "Build";
            tower = (GameObject)Resources.Load("Flute");

        }
        if (moving == 2)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, target, 4.0f * Time.deltaTime);
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
                moving = 1;
                target = Input.mousePosition;
            }
        }
    }

    public bool isClose()
    {
        if (Vector2.Distance(this.transform.position, target) < 6f)
        {
            return true;
        }else{
            return false;
        }
    }

    void Harmonize()
    {

    }
}
