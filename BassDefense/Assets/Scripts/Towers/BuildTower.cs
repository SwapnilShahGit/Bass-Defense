using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public Transform t;
    int building = 0;
    public int placed = 0;
    public bool buildable;
    Color old;
    int i;
    private GameObject radius;

    public Texture2D cursor;
    public Texture2D hcursor;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((building == 1 && Vector2.Distance(GameObject.FindWithTag("Player").GetComponent<Transform>().position, this.gameObject.transform.position) < 2f) && PlayerController.moving == 2)
        {
            GameObject d = Instantiate(PlayerController.tower, this.transform);
            placed = 1;

            d.transform.localScale = new Vector3(0.50f, 0.50f, 1);
            PlayerController.mode = "Slashy";
            building = 0;
            PlayerController.moving = 0;
        }

    }



    void OnMouseEnter()
    {
        old = this.gameObject.GetComponent<SpriteRenderer>().color;
        if (PlayerController.mode == "Build")
        {
            Cursor.SetCursor(hcursor, Vector2.zero, CursorMode.Auto);

            float towersRadius = PlayerController.tower.GetComponent<CircleCollider2D>().radius;
            radius = Instantiate((GameObject)Resources.Load("Radius"), this.transform);
            radius.transform.localScale = new Vector3(towersRadius, towersRadius, 0);

            if (buildable)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Destroy(radius);
        this.gameObject.GetComponent<SpriteRenderer>().color = old;
    }

    void OnMouseUp()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Destroy(radius);
        if (PlayerController.mode == "Build" && placed == 0 && buildable)
        {
            PlayerController.target = this.gameObject.transform.position;
            PlayerController.moving = 2;
            building = 1;
            //d.transform.localScale -= new Vector3(0.95f,0.95f,0);
        }
        if (placed == 1)
        {
            //upgrade
        }

    }
}
