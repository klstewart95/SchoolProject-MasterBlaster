using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Anim : MonoBehaviour {
    public Transform Pickup;
    Rigidbody2D rb;
    public bool isPicked;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (isPicked)
        {
            rb.velocity = new Vector2(rb.velocity.x, 2);
        }

		
	}

    void OnTriggerEnter2D(Collider2D c)
    {

        Debug.Log(c.gameObject.tag);


        switch (c.gameObject.tag)
        {
            case "Player":
                isPicked = true;
                Destroy(gameObject,0.2f);
                break;
        }


    }

}
