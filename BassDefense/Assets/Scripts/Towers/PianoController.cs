﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoController : TowerController
{

    List<GameObject> bullets;
    float timeint;
    float time;
    float onCD;
    public int burstnum;
    int i;
    // Use this for initialization
    void Start()
    {
        ClassicalAudioController.activepianos++;
        onCD = 0;
        timeint = 0;
        PlayerController.flow -= cost;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        i = 0;
        foreach (GameObject target in targets)
        {
            
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
                shoot(target);
                if (i >= burstnum)
                {
                    onCD = 1;
                    time = Time.time;
                }
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            targets.Add(other.gameObject);
            //Debug.Log("target added");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            targets.Remove(other.gameObject);
            //Debug.Log("target removed");
        }
    }

    void shoot(GameObject enemy)
    {
        GameObject b = Instantiate((GameObject)Resources.Load("Bullet"));
        b.transform.position = this.gameObject.transform.position;
        if (b.GetComponent<BulletBehaviour>() != null && enemy != null)
        {
            b.GetComponent<BulletBehaviour>().target = enemy;
            b.GetComponent<BulletBehaviour>().dmg = damage;
        }


    }

}

