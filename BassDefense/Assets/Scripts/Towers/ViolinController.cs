using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinController : TowerController
{

    List<GameObject> bullets;
    float timeint;
    float time;
    float onCD;
    // Use this for initialization
    void Start()
    {
        ModernAudioController.activeviolins++;
        ClassicalAudioController.activeviolins++;
        onCD = 0;
        timeint = 0;
        PlayerController.flow -= cost;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

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
                shoot(targets[0]);
                onCD = 1;
                time = Time.time;
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
        GameObject b = Instantiate((GameObject)Resources.Load("ViolinBullet"));
        b.transform.position = this.gameObject.transform.position;
        if (b.GetComponent<BulletBehaviour>() != null && enemy != null)
        {
            b.GetComponent<BulletBehaviour>().effect = "slow";
            b.GetComponent<BulletBehaviour>().target = enemy;
            b.GetComponent<BulletBehaviour>().dmg = damage;
        }


    }

}

