using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumController : TowerController
{
    public Sprite Upgraded;

    float timeint;
    float time;
    float bulletTravel;
    int onCD;

    // Use this for initialization
    void Start()
    {
        AudioController.activedrums++;
        onCD = 0;
        timeint = 0;
        PlayerController.flow -= cost;
        time = Time.time;
        bulletTravel = GetComponent<CircleCollider2D>().radius / 2;
    }

    // Update is called once per frame
    void Update()
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
            foreach (GameObject target in targets)
            {
                target.GetComponent<EnemyController>().hp -= damage;
                if (target.GetComponent<EnemyController>().slow == false)
                {
                    target.GetComponent<EnemyController>().slow = true;
                    target.GetComponent<EnemyController>().speed *= 0.75f;
                }
                onCD = 1;
                time = Time.time;
            }
            if (targets.Count > 0)
            {
                float x = this.gameObject.transform.position.x;
                float y = this.gameObject.transform.position.y;
                Bullets(x, y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            targets.Add(other.gameObject);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy") && targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
            other.gameObject.GetComponent<EnemyController>().slow = false;
            other.gameObject.GetComponent<EnemyController>().speed *= 1.333f;
        }

    }

    void Bullets(float x, float y)
    {
        GameObject wave1 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave1.transform.position = this.gameObject.transform.position;
        if (wave1.GetComponent<BulletBehaviour>() != null)
        {
            wave1.GetComponent<BulletBehaviour>().destination = new Vector3(x - bulletTravel, y);
        }

        GameObject wave2 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave2.transform.position = this.gameObject.transform.position;
        if (wave2.GetComponent<BulletBehaviour>() != null)
        {
            wave2.GetComponent<BulletBehaviour>().destination = new Vector3(x + bulletTravel, y);
        }
        GameObject wave3 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave3.transform.position = this.gameObject.transform.position;
        if (wave3.GetComponent<BulletBehaviour>() != null)
        {
            wave3.GetComponent<BulletBehaviour>().destination = new Vector3(x, y - bulletTravel);
        }
        GameObject wave4 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave4.transform.position = this.gameObject.transform.position;
        if (wave4.GetComponent<BulletBehaviour>() != null)
        {
            wave4.GetComponent<BulletBehaviour>().destination = new Vector3(x, y + bulletTravel);
        }
        GameObject wave5 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave5.transform.position = this.gameObject.transform.position;
        if (wave5.GetComponent<BulletBehaviour>() != null)
        {
            wave5.GetComponent<BulletBehaviour>().destination = new Vector3(x - bulletTravel, y - bulletTravel);
        }
        GameObject wave6 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave6.transform.position = this.gameObject.transform.position;
        if (wave6.GetComponent<BulletBehaviour>() != null)
        {
            wave6.GetComponent<BulletBehaviour>().destination = new Vector3(x - bulletTravel, y + bulletTravel);
        }
        GameObject wave7 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave7.transform.position = this.gameObject.transform.position;
        if (wave7.GetComponent<BulletBehaviour>() != null)
        {
            wave7.GetComponent<BulletBehaviour>().destination = new Vector3(x + bulletTravel, y - bulletTravel);
        }
        GameObject wave8 = Instantiate((GameObject)Resources.Load("BulletDrum"));
        wave8.transform.position = this.gameObject.transform.position;
        if (wave8.GetComponent<BulletBehaviour>() != null)
        {
            wave8.GetComponent<BulletBehaviour>().destination = new Vector3(x + bulletTravel, y + bulletTravel);
        }
    }


}
