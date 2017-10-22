using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    int building = 0;
    public GameObject player;
    public int placed = 0;
    Color old;
    int i;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((building == 1  && Vector2.Distance(GameObject.FindWithTag("Player").GetComponent<Transform>().position,this.gameObject.transform.position) < 2f) && PlayerController.moving == 2)
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
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }



    }
    void OnMouseExit()
    {

        this.gameObject.GetComponent<SpriteRenderer>().color = old;
    }
    void OnMouseUp()
    {
        if (PlayerController.mode == "Build" && placed == 0)
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
