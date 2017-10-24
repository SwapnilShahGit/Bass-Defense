using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RapidFireCooldown : MonoBehaviour {
    public RapidFire rapid;
    public Image rapidimage;
    public Text t;
	// Use this for initialization
	void Start () {
        t.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (RapidFire.onCD == 1)
        {
            t.text = (Mathf.RoundToInt(Mathf.Abs(rapid.timeint - rapid.cd))).ToString() + "s";
        }
        else
        {
            t.text = "";
        }
        if (RapidFire.isActive)
        {
            t.text = "Active";
        }
        
	}
}
