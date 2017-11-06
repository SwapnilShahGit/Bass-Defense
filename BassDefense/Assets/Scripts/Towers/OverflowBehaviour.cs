using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    int dmg = 100;
    // Use this for initialization
    void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, target, speed * Time.deltaTime);
 
        }

        if (Vector2.Distance(this.gameObject.transform.position, target) < 1.5f)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(target, 10);
            foreach (Collider2D enemycol in enemies)
            {
                GameObject e = enemycol.gameObject;
                if (e.GetComponent<EnemyController>() != null)
                {
                    e.GetComponent<EnemyController>().hp -= dmg;
                }
            }
            Destroy(this.gameObject);
        } 

    }
}
