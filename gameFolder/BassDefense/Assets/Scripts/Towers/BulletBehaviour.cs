using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public Vector3 destination;
    public int dmg;
    public string effect;
    Vector3 oldpos;
    // Use this for initialization
    void Start()
    {
        speed = 6f;
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
            oldpos = target.transform.position;

            if (Vector3.Distance(this.transform.position, target.transform.position) == 0)
            {
                if (target.GetComponent<EnemyController>() != null)
                {
                    target.GetComponent<EnemyController>().hp -= dmg;
                    if (effect == "slow")
                    {
                        if (target.GetComponent<EnemyController>().slow == false)
                        {
                            target.GetComponent<EnemyController>().slow = true;
                            target.GetComponent<EnemyController>().speed *= 0.6f;
                            target.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                    }

                    Destroy(this.gameObject);
                }
            }
        }
        if (destination != null && target == null && destination != new Vector3(0,0,0))
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, destination, speed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, destination) == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, oldpos, speed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, oldpos) == 0 || oldpos == new Vector3(0, 0, 0))
            {
                Destroy(this.gameObject);
            }
        }

    }
}
