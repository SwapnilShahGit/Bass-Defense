using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int hp;
    int maxhp;
    public int bounty = 5;
    public float speed = 2;
    public int damage = 25;
    public GameObject enemy;
    public GameObject pBase;
    Transform hpBar;
    float origscaley;
	// Use this for initialization
	void Start () {
        hpBar = transform.GetChild(0);
        maxhp = hp;
        origscaley = hpBar.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.localScale = new Vector3(2f,(origscaley*hp/maxhp), 1);
        
        if (hp <= 0)
        {
            FloatingTextController.bounty(bounty, this.transform.position.x, this.transform.position.y);
            PlayerController.money += bounty;
            Destroy(enemy);

        }
        
        enemy.GetComponent<Transform>().position = Vector2.MoveTowards(enemy.GetComponent<Transform>().position, pBase.transform.position, speed * Time.deltaTime);
	}
}
