using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {
    public int hp = 100;
    public GameController gameController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.GetChild(0).transform.localScale = new Vector3((hp / 100f) * 7 , 0.5f, 1);
        if (hp <= 0)
        {
            gameController.end();
        }
	}
}
