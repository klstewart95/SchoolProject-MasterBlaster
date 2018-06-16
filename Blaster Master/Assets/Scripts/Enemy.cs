using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Rigidbody2D rb;

    public float health;
    public bool attacking;
    public Transform projectileSpawnPoint01;
    public Projectile projectilePrefab;
    public float projectileForce;

    public bool isLeft;

    public float HealthBar;

    public Transform HP;

    public bool isHit;

    Animator anim;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        if (projectileForce == 0)
        {
            projectileForce = 2.0f;
            Debug.Log("projectileForce was not set Defaulting to " + projectileForce);
        }

  

        if (health == 0)
        {
            health = 100;
        }
		
	}
	
	// Update is called once per frame
	void Update () {


        if (isHit)
        {
            if (health <= 75)
            {
                HP.transform.localScale = new Vector3(0.75f, 1, 1);
            }

            if (health <= 50)
            {
                HP.transform.localScale = new Vector3(0.50f, 1, 1);
            }

            if (health <= 25)
            {
                HP.transform.localScale = new Vector3(0.25f, 1, 1);
            }

            if (health <= 0)
            {
                HP.transform.localScale = new Vector3(0, 1, 1);
                Destroy(gameObject, 1);
            }
            isHit = false;     
        }
        /*
        if (health <= 75)
        {
            HP.transform.localScale += new Vector3(0.75f, 1, 1);
        }

        if (health <= 50)
        {
            HP.transform.localScale += new Vector3(0.50f, 1, 1);
        }

        if (health <= 25)
        {
            HP.transform.localScale += new Vector3(0.25f, 1, 1);
        }

        if (health <= 0)
        {
            Destroy(gameObject, 1);
        }
        */

        anim.SetFloat("HP", Mathf.Abs(health));




        if (!attacking)
        {
            attacking = true;
            Fire();
        }

        rb.velocity = new Vector2(-0.2f, rb.velocity.y);


    }

    void OnTriggerEnter2D(Collider2D c)
    {

        Debug.Log(c.gameObject.tag);

        if (c.gameObject.tag == "PlayerBullet")
        {
            isHit = true;
            health = health - 25;
        }
        if (c.gameObject.tag == "EndZone")
        {
            health = 0;
        }

    }

    void Fire()
    {

        float direction = Random.Range(1, 90);
        if (direction == 1)
        {
            Projectile temp =
            Instantiate(projectilePrefab,
            projectileSpawnPoint01.position,
            projectileSpawnPoint01.rotation);
            temp.speed = projectileForce;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 0);
            attacking = false;
        }

        else if (direction == 2)
        {
            Projectile temp =
            Instantiate(projectilePrefab,
            projectileSpawnPoint01.position,
            projectileSpawnPoint01.rotation);
            temp.speed = projectileForce;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, -3);
            attacking = false;
        }

        else if (direction == 3)
        {
            Projectile temp =
            Instantiate(projectilePrefab,
            projectileSpawnPoint01.position,
            projectileSpawnPoint01.rotation);
            temp.speed = projectileForce;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, -5);
            attacking = false;
        }

        else
        {
            attacking = false;
        }

    }


    IEnumerator RandWait (int a, int b)
    {
       
        yield return new WaitForSeconds(Random.Range(a, b));
      
    }

}
