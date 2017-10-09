using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int hp;
    public int bounty = 5;
    public float speed = 2;
    public int damage = 25;
    public GameObject enemy;
    public GameObject pBase;
    GameObject hpBar;
	// Use this for initialization
	void Start () {
		hpBar = Instantiate((GameObject)Resources.Load("HealthBar"));
        hpBar.transform.parent = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.transform.localScale = new Vector3((hp / 10f), 1f, 1);
        if (Vector2.Distance(this.gameObject.transform.position,pBase.transform.position)<1.5f)
        {
            pBase.GetComponent<BaseController>().hp -= damage;
            Destroy(enemy);
        }
        if (hp <= 0)
        {
            Destroy(enemy);

        }
        enemy.GetComponent<Transform>().position = Vector2.MoveTowards(enemy.GetComponent<Transform>().position, pBase.transform.position, speed * Time.deltaTime);
	}
}
