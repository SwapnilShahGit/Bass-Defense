using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // UI tingz
    public Slider healthSlider;
    public Slider manaSlider;
    public Text playerMoneyText;

    public Texture2D cursor;


    public float cd = 0.7f;
    int onCD = 0;
    float time = 0;
    float timeint = 0;
    public static int mana;
    public int damage = 5;
    public static GameObject player;
    public static Vector2 target;
    public static EnemyController attacking;
    private Vector3 position;
    public static GameObject tower;
    public static int moving;
    public static int money;
    public static int health;
    public static string mode = "Slashy";
    public static float speed;


    Animator animator;
    bool isLookingRight = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {

        player = gameObject;
        speed = 2.0f;
        time = Time.time;
        moving = 0;
        money = 10;
        health = 100;
        mana = 10;
        StartCoroutine(regenhp());
        StartCoroutine(regenmana());
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
        playerMoneyText = GameObject.Find("Money").GetComponent<Text>();
        healthSlider.value = health;
        manaSlider.value = mana;
        playerMoneyText.text = money.ToString();
    }

    void Update()
    {
        // AWSD CONTROLS
        Vector2 move = Vector2.zero;

        if (Input.GetKey("a"))
        {
            move.x -= 1f;
        }
        if (Input.GetKey("d"))
        {
            move.x += 1f;
        }
        if (Input.GetKey("w"))
        {
            move.y += 1f;
        }
        if (Input.GetKey("s"))
        {
            move.y -= 1f;
        }

        if (moving == 0)
        {
            this.transform.Translate(move * speed * Time.deltaTime);
        }
        else if (moving == 1)
        {
            target += move * speed;
        }

        if (Input.GetKeyDown("e"))
        {
            if (money >= 5)
            {
                mode = "Build";
                tower = (GameObject)Resources.Load("Drum");
            }

        }
        if (Input.GetKeyDown("q"))
        {
            if (money >= 10)
            {
                mode = "Build";
                tower = (GameObject)Resources.Load("Flute");
            }

        }
        if (moving == 2)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
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
                if (onCD == 0)
                {
                    if (isLookingRight)
                    {
                        //look right
                        animator.SetInteger("AnimState", 0);
                    }
                    else
                    {
                        //look left
                        animator.SetInteger("AnimState", 2);
                    }
                }
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
                    //attack right
                    if (isLookingRight)
                    {
                        animator.SetTrigger("Attack");
                    }
                    //attack left
                    else
                    {
                        animator.SetTrigger("Attack");
                    }
                    moving = 0;
                    attacking.hp -= damage;
                    onCD = 1;
                    time = Time.time;
                }
            }
            else
            {
                if (transform.position.x > pos.x)
                {
                    //look left
                    animator.SetInteger("AnimState", 2);
                    isLookingRight = false;
                }
                if (transform.position.x < pos.x)
                {
                    //look right
                    animator.SetInteger("AnimState", 0);
                    isLookingRight = true;
                }
                this.transform.position = Vector2.MoveTowards(this.transform.position, pos, speed * Time.deltaTime);
            }

        }
        if (mode == "Slashy")
        {

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
        else if (mode == "Build" || mode == "Ability")
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
        healthSlider.value = health;
        manaSlider.value = mana;
        playerMoneyText.text = money.ToString();
    }

    public bool isClose()
    {
        if (Vector2.Distance(this.transform.position, target) < 6f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator regenhp()
    {
        while (health >= 0)
        {
            yield return new WaitForSeconds(3);
            if (health < 100)
            {
                health++;
            }
        }
    }
    IEnumerator regenmana()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            if (mana < 10)
            {
                mana++;
            }
        }
    }


    public void drumbutton()
    {
        if (money >= 5)
        {
            mode = "Build";
            tower = (GameObject)Resources.Load("Drum");
        }
    }

    public void flutebutton()
    {
        if (money >= 10)
        {
            mode = "Build";
            tower = (GameObject)Resources.Load("Flute");
        }
    }



}
