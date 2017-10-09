using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public GameObject player;
    Color old;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnMouseEnter()
    {
        old = this.gameObject.GetComponent<SpriteRenderer>().color;
        if (PlayerController.mode == "Build")
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }


    }
    void OnMouseExit()
    {

        this.gameObject.GetComponent<SpriteRenderer>().color = old;
    }
    void OnMouseUp()
    {
        if (PlayerController.mode == "Build")
        {
            PlayerController.target = this.gameObject.transform.position;
            while (Vector2.Distance(player.transform.position, this.gameObject.transform.position) < 5f)
            {
            }
            PlayerController.moving = 0;
            GameObject d = Instantiate(PlayerController.tower, this.transform);
            d.transform.localScale = new Vector3(0.50f, 0.50f, 1);

            //d.transform.localScale -= new Vector3(0.95f,0.95f,0);
        }

    }
}
