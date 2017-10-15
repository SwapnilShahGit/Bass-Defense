using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseController : MonoBehaviour {
    // UI tingz
    public Text baseHealthText;
    public Image damageFlash;                                   // Reference to an image to flash on the screen on being hurt.
    public bool damaged = false;                                // True when the base gets damaged.
    public float flashSpeed = 100000f;                             // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    public UnityEvent OnBaseDestroyed;

    public int maxhp = 100;
    int hp;
    Transform hpBar;
    float origscalex;
 
	// Use this for initialization
	void Start () {
        hp = maxhp;

        damageFlash = GameObject.Find("DamageFlash").GetComponent<Image>();
        baseHealthText = GameObject.Find("BaseHealth").GetComponent<Text>();
        baseHealthText.text = hp.ToString();

        hpBar = this.gameObject.transform.GetChild(0);
        origscalex = hpBar.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.localScale = new Vector3( (origscalex*hp/maxhp),2f,1);
        if (hp <= 0)
        {
            OnBaseDestroyed.Invoke();
        }

        if (damaged)
        {
            damageFlash.color = flashColour;
        }
        else
        {
            // ... transition the colour back to clear.
            damageFlash.color = Color.Lerp(damageFlash.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "enemy")
        {
            hp -= coll.gameObject.GetComponent<EnemyController>().damage;
            Destroy(coll.gameObject);
            baseHealthText.text = hp.ToString();
            damaged = true;
        }
    }

   
}
