using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : AbilityController {
    public static RapidFire rapid = null;
    private void Awake()
    {
        if (rapid != null && rapid != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            rapid = this;
        }
    }
    float dur = 4;
    GameObject[] towers;
    float casttime;
    float castint;
    bool isActive;
    float cdmult = 0.4f;
	// Use this for initialization
	void Start () {
        isActive = false;
        cost = 5;
        cd = 34;
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeint = Time.time - time;
        if (timeint > cd)
        {
            onCD = 0;
        }
        if (isActive)
        {
            castint = Time.time - casttime;
            if (castint > dur)
            {
                foreach (GameObject t in towers)
                {
                    t.GetComponent<TowerController>().cd /= cdmult;
                }
                isActive = false;
                onCD = 1;
                time = Time.time;
            }
        }
	}

    public void cast()
    {
        if (onCD == 0 && PlayerController.mana >= 5)
        {
            PlayerController.mana -= 5;
            Debug.Log("Casting");
            towers = GameObject.FindGameObjectsWithTag("Tower");
            casttime = Time.time;
            isActive = true;
            foreach (GameObject t in towers)
            {
                t.GetComponent<TowerController>().cd *= cdmult;
            }
        }
        else
        {
            //on cooldown sound
        }
    }
}
