﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public GameObject target;
    public int dmg;
    Vector3 oldpos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (target != null)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, target.transform.position, 8.0f * Time.deltaTime);
            oldpos = target.transform.position;

            if (Vector3.Distance(this.transform.position, target.transform.position) == 0)
            {
                if (target.GetComponent<EnemyController>() != null)
                {
                    target.GetComponent<EnemyController>().hp -= dmg;
                    Destroy(this.gameObject);
                }
                
            }

        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, oldpos, 8.0f * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, oldpos) == 0){
                Destroy(this.gameObject);
            }
        }
	}
}
