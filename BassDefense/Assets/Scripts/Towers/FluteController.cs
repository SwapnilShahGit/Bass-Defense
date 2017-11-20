using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteController : TowerController
{
    public bool isRapper = false;
    List<GameObject> bullets;
    float timeint;
    float time;
    float onCD;
    // Use this for initialization
    void Start()
    {
        AudioController.activeflutes++;
        onCD = 0;
        timeint = 0;
        PlayerController.flow -= cost;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyController mostDangerousEnemy = null;
        foreach(GameObject target in targets) {
            EnemyController enemyTarget = target.GetComponent<EnemyController>();

            if(mostDangerousEnemy == null) {
                mostDangerousEnemy = enemyTarget;
            }
            else {
                if(enemyTarget.GetDistanceTravelled() > mostDangerousEnemy.GetDistanceTravelled()) {
                    mostDangerousEnemy = enemyTarget;
                }
            }
        }

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
            if(mostDangerousEnemy != null) {
                shoot(mostDangerousEnemy.gameObject);
                onCD = 1;
            }
            time = Time.time;
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
        GameObject b;
        if (!isRapper)
        {
            b = Instantiate((GameObject)Resources.Load("Bullet"));
        }
        else
        {
            b = Instantiate((GameObject)Resources.Load("Money"));
        }
        b.transform.position = this.gameObject.transform.position;
        if (b.GetComponent<BulletBehaviour>() != null && enemy != null)
        {
            b.GetComponent<BulletBehaviour>().target = enemy;
            b.GetComponent<BulletBehaviour>().dmg = damage;
        }


    }

}

