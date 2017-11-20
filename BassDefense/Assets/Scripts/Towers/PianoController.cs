using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoController : TowerController
{

    List<GameObject> bullets;
    float timeint;
    float time;
    float onCD;
    public int burstnum;
    public float burstcd;
    int onburstCD;
    int i;
    // Use this for initialization
    void Start()
    {
        burstcd = 1f;
        ModernAudioController.activepianos++;
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
                    i = 0;
                    onCD = 0;
                }
            }
            else if (onburstCD == 1)
            {
                if (timeint > burstcd)
                {
                    onburstCD = 0;
                    
                }
            }
            else
            {
                i++;
                if (i > burstnum)
                {
                    onCD = 1;
                    time = Time.time;
                }
                else
                {
                    StartCoroutine(focus(target));
                    onburstCD = 1;
                    time = Time.time;
                }
            }

        }
    }

    IEnumerator focus(GameObject target)
    {
        while (target != null)
        {
            yield return new WaitForSeconds(0.3f);
            shoot(target);
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
        GameObject b = Instantiate((GameObject)Resources.Load("PianoBullet"));
        b.transform.position = this.gameObject.transform.position;
        if (b.GetComponent<BulletBehaviour>() != null && enemy != null)
        {
            b.GetComponent<BulletBehaviour>().target = enemy;
            b.GetComponent<BulletBehaviour>().dmg = damage;
        }


    }

}

