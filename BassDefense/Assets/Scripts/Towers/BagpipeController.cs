using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetController : TowerController {
    public Sprite Upgraded;

    float timeint;
    float time;
    int onCD;
    
	// Use this for initialization
	void Start () {
        AudioController.activedrums++;
        onCD = 0;
        timeint = 0;
        PlayerController.money -= cost;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        
        
       
        
        timeint = Time.time - time;
        
        
            if (onCD == 1)
            {
                if (timeint > cd)
                {
                    onCD = 0;
                }
            }
            else
            {
                foreach (GameObject target in targets)
                {
                target.GetComponent<EnemyController>().hp -= damage;
                onCD = 1;
                time = Time.time;
                //add effect
                }
            }
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy")){
            targets.Add(other.gameObject);
        }
        
    }
    
    void OnTriggerExit2D( Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy")){
            targets.Remove(other.gameObject);
        }
        
    }
    
    
}
