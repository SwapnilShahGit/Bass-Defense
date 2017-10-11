using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseController : MonoBehaviour {
    public UnityEvent OnBaseDestroyed;

    public int maxhp = 100;
    public int hp = 100;
    public Transform hpBarPrefab;
    public Transform healthBarHolder;
    Transform hpBar;
    float origscalex;
 
	// Use this for initialization
	void Start () {
        hpBar = Instantiate(hpBarPrefab, healthBarHolder.position, healthBarHolder.rotation) as Transform;
        hpBar.parent = transform;
        origscalex = hpBar.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.localScale = new Vector3(origscalex * hp / maxhp,4,1);
        if (hp <= 0)
        {
            OnBaseDestroyed.Invoke();
        }
	}
}
