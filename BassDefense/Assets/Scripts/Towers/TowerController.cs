using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {
    GameObject tower;
    public int cost;
    public int upgrade;
    public int damage;
    public int level;
    public float cd;
    public List<GameObject> targets;
    public bool pinrange = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    /*
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            targets.Add(other.gameObject);
            Debug.Log("target added");
        }
        if (other.gameObject.tag.Equals("Player"))
        {
            pinrange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            targets.Remove(other.gameObject);
            Debug.Log("target removed");
        }
        if (other.gameObject.tag.Equals("Player")){
            pinrange = false;
        }
    }
    */
}
