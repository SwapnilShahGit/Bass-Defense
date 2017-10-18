using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    // UI tingz
    public Text playerHealthText;
    public Text playerMoneyText;
    public float cd = 0.7f;
    int onCD = 0;
    float time = 0;
    float timeint = 0;
    public int damage = 2;
    public static GameObject player;
    public static Vector2 target;
    public static EnemyController attacking;
	private Vector3 position;
	public static GameObject tower;
    public static int moving;
	public static int money;
    public static int health;
    public static string mode = "Slashy";

	void Start () {
        time = Time.time;
		moving = 0;
		money = 10;
        health = 100;
        playerHealthText = GameObject.Find("PlayerHealth").GetComponent<Text>();
        playerMoneyText = GameObject.Find("Money").GetComponent<Text>();
        playerHealthText.text = health.ToString();
        playerMoneyText.text = money.ToString();
    }

    void Update () {
		if (Input.GetKeyDown ("e")) {
            if (money >= 5)
            {
                mode = "Build";
                tower = (GameObject)Resources.Load("Drum");
            }
           
		}
        if (Input.GetKeyDown("q"))
        {
            if (money >= 10){
            mode = "Build";
            tower = (GameObject)Resources.Load("Flute");
            }

        }
        if (moving == 2)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, target, 4.0f * Time.deltaTime);
        }

        if (moving == 1)
        {

            if (attacking != null)
            {
                target = attacking.GetComponent<Transform>().position;
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(target);
            if (attacking != null)
            {
                pos = attacking.GetComponent<Transform>().position;
            }
            if (Vector2.Distance(this.transform.position, pos) < 1f && attacking != null)
            {

                timeint = Time.time - time;
                if (onCD == 1)
                {
                    moving = 0;
                    if (timeint > cd)
                    {
                        onCD = 0;
                    }
                }
                else
                {
                    moving = 0;
                    attacking.hp -= damage;
                    onCD = 1;
                    time = Time.time;
                }
            }
            else
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, pos, 4.0f * Time.deltaTime);
            }
            
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
        playerHealthText.text = health.ToString();
        playerMoneyText.text = money.ToString();
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
