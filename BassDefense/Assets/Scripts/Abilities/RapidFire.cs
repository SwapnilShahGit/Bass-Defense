using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : MonoBehaviour {

    static int onCD;
    int cost;
    float dur;
    static TowerController[] towers;
    static float casttime;
    float castint;
    static bool isActive;
    static float cdmult;
    float time;
    float timeint;
    float cd;
	// Use this for initialization
	void Start () {
        onCD = 0;
        
        isActive = false;
        dur = 5;
        cdmult = 0.4f;
        cost = 5;
        cd = 30;
        time = Time.time;
        timeint = 0;
        casttime = 0;
        castint = 0;
        towers = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            cast();
        }

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
                foreach (TowerController t in towers)
                {
                    t.cd /= cdmult;
                }
                isActive = false;
                onCD = 1;
                time = Time.time;
            }
        }
	}

    public static void cast()
    {
        if (onCD == 0 && PlayerController.mana >= 5)
        {
            PlayerController.mana -= 5;
            Debug.Log("Casting");
            towers = (TowerController[])GameObject.FindObjectsOfType(typeof(TowerController));
            casttime = Time.time;
            isActive = true;
            foreach (TowerController t in towers)
            {
                t.cd *= cdmult;
            }
        }
        else
        {
            //on cooldown sound
        }
    }
}
