using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int hp;
    public float speed = 2;
    public GameObject enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
        {
            Destroy(enemy);
        }
        enemy.GetComponent<Transform>().position = Vector2.MoveTowards(enemy.GetComponent<Transform>().position, new Vector2(11, 4), speed * Time.deltaTime);
	}
}
