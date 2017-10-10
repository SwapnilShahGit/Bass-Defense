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
    public GameObject hpBar;
    float origscalex;
	// Use this for initialization
	void Start () {
        maxhp = hp;
        origscalex = hpBar.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.transform.localScale = new Vector3(origscalex * hp / maxhp, 0.5f, 1);
        if (Vector2.Distance(this.gameObject.transform.position,pBase.transform.position)<1.5f)
        {
            pBase.GetComponent<BaseController>().hp -= damage;
            Destroy(hpBar);
            Destroy(enemy);
        }
        if (hp <= 0)
        {
            FloatingTextController.bounty(bounty, this.transform.position.x, this.transform.position.y);
            PlayerController.money += bounty;
            Destroy(enemy);

        }
        enemy.GetComponent<Transform>().position = Vector2.MoveTowards(enemy.GetComponent<Transform>().position, pBase.transform.position, speed * Time.deltaTime);
	}
}
