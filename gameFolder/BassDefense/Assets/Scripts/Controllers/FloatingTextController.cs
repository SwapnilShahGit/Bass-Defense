using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public  class FloatingTextController : MonoBehaviour {

    public static void bounty(int gold,float x, float y)
    {
        
        GameObject ftext = Instantiate((GameObject)Resources.Load("GoldText"));
        Vector2 pos = Camera.main.WorldToScreenPoint(new Vector2(x + Random.Range(-0.2f, 0.2f), y + Random.Range(-0.2f, 0.2f)));
        ftext.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        ftext.transform.position = pos;
        ftext.GetComponent<Text>().text = "+" + gold.ToString();
        Destroy(ftext, 0.5f); 
    }
}
