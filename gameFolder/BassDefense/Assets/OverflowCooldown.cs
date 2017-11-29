using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverflowCooldown : MonoBehaviour
{
    public Overflow overflow;
    public Text t;
    // Use this for initialization
    void Start()
    {
        t.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Overflow.onCD == 1)
        {
            t.text = (Mathf.RoundToInt(Mathf.Abs(overflow.timeint - overflow.cd))).ToString() + "s";
        }
        else
        {
            t.text = "";
        }
    }
}
