﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int hp;
    public Texture2D scursor;
    public Texture2D cursor;
    int maxhp;
    public int bounty = 5;
    public float speed = 2;
    public int damage = 25;
    public GameObject enemy;
    public int pdmg = 5;
    float cd = 2;
    int onCD = 1;
    float time;
    float timeint;
    Transform hpBar;
    float origscaley;

    Vector3[] path;
    int targetIndex;

	// Use this for initialization
	void Start () {
        hpBar = transform.GetChild(0);
        maxhp = hp;
        origscaley = hpBar.localScale.y;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(gameObject.transform.position, PlayerController.player.GetComponent<Transform>().position) < 2f)
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
                PlayerController.health -= pdmg;
                onCD = 1;
                time = Time.time;
            }
        }
        hpBar.localScale = new Vector3(2f,(origscaley*hp/maxhp), 1);
        
        if (hp <= 0)
        {
            FloatingTextController.bounty(bounty, this.transform.position.x, this.transform.position.y);
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            Destroy(enemy);
            PlayerController.money += 5;
        }
	}

    public void GoToTarget(Vector3 target) {
        PathRequestManager.RequestPath(transform.position, target, OnPathFound, true);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if(pathSuccessful) {
            path = newPath;
            if(this != null) {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
        else {
            Debug.Log("Path unsuccessful");
        }
    }

    

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while(true) {
            if(transform.position == currentWaypoint) {
                targetIndex++;
                if(targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(scursor, Vector2.zero, CursorMode.Auto);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlayerController.attacking = this;
        }
    }
}
