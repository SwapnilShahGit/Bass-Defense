using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {
    public int maxhp = 100;
    public int hp = 100;
    public GameController gameController;
    public GameObject hpBar;
    float origscalex;
 
	// Use this for initialization
	void Start () {
        origscalex = hpBar.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.transform.localScale = new Vector3(origscalex * hp / maxhp,4,1);
        if (hp <= 0)
        {
            gameController.End();
        }
	}
}
