using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {

    Rigidbody2D rb;  // creates refrence to this function dont initilize here

    

    public float speed;

    public float JumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public bool DoubleJump;
    public bool MegaJump;

    public Transform projectileSpawnPoint01;
    public Transform projectileSpawnPoint02;
    public Projectile projectilePrefab;
    public float projectileForce;

    public SpriteRenderer Health01;
    public SpriteRenderer Health02;
    public SpriteRenderer Health03;
    public SpriteRenderer Health04;
    public SpriteRenderer Health05;

    public float MPU;

    public float Score;


    public bool isLeft;
    public bool isUp;

    public float PHealth;

    public bool isAlive;

    Animator anim;

    // Use this for initialization
    void Start () {

        MPU = 0;

        Score = 0;
        
        rb = GetComponent<Rigidbody2D>();  // initilized RigidBody makes rb able to access everything attached to RigidBody
        if (!rb)
        {
            Debug.LogWarning("No RigidBody Found" + name);
        }

        if (speed <= 0)
        {
            Debug.LogWarning("No Speed Set  Speed = < 1  Set Speed To " + speed + " On " + name);
            speed = 1.0f;
        }

        if (PHealth == 0)
        {
            PHealth = 100;
        }




        if (JumpForce <= 0)
        {
            Debug.LogWarning("No Jump " + name);
            JumpForce = 3.0f;
        }

        if (!groundCheck)
        {
            Debug.LogWarning("No GroundCheck Found " + name);
        }

        if (groundCheckRadius <= 0)
        {
            Debug.LogWarning("No Check Radius " + name);
            groundCheckRadius = 0.2f;
        }





        anim = GetComponent<Animator>();  // initilized RigidBody makes rb able to access everything attached to RigidBody
        if (!anim)
        {
            Debug.LogWarning("No Animator Found" + name);
        }



        if (!projectileSpawnPoint01)
            Debug.LogError("Missing projectileSpawn");
        if (!projectileSpawnPoint02)
            Debug.LogError("Missing projectileSpawn");

        if (!projectilePrefab)
            Debug.LogError("Missing projectilePrefab");
        if (projectileForce == 0)
        {
            projectileForce = 5.0f;
            Debug.Log("projectileForce was not set Defaulting to " + projectileForce);
        }



    }

    // Update is called once per frame
    void Update () {
        
        float moveValue = Input.GetAxisRaw("Horizontal"); // Setting float to either -1 (Left)  0 (None)  1 (Right)
        float aimValue = Input.GetAxisRaw("Vertical");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer); //checks the ingame ground check to check to jump or not




        if (Input.GetButtonDown("Jump") && isGrounded)
        {  // initial jump
            if (MegaJump)
            {
                JumpForce = 5f;
            }
            DoubleJump = true;
            Debug.Log("Jump Selected");
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

            if (PHealth > 80){
                Health05.color = new Color(1f, 1f, 1f, 1f);
            }
            if (PHealth <= 80){
                Health05.color = new Color(1f, 1f, 1f, 0);
            }


            if (PHealth > 60){
                Health04.color = new Color(1f, 1f, 1f, 1f);
            }
            if (PHealth <= 60){ 
                Health04.color = new Color(1f, 1f, 1f, 0);
            }


            if (PHealth > 40) { 
                Health03.color = new Color(1f, 1f, 1f, 1f);
            }
            if (PHealth <= 40){
                Health03.color = new Color(1f, 1f, 1f, 0);
            }


            if (PHealth > 20){
                Health02.color = new Color(1f, 1f, 1f, 1f);
            }
            if (PHealth <= 20) {          
                Health02.color = new Color(1f, 1f, 1f, 0);
            }


            if (PHealth > 10){
                Health01.color = new Color(1f, 1f, 1f, 1f);
            }
            if (PHealth <= 10){
                Health01.color = new Color(1f, 1f, 1f, 0);
            }


             if (PHealth <= 0){
                Destroy(gameObject,0.5f);
            }
 



        if (Input.GetButtonDown("Jump") && !isGrounded && DoubleJump) // double jump
        {
            speed = 1f;
            JumpForce = 3f;
            DoubleJump = false;
            Debug.Log("Jump Selected");
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }



        if (!isGrounded && MegaJump)
        {
            speed = 3f;
            MegaJump = false;
        }
        else if (isGrounded && !MegaJump)
        {
            speed = 1f;
            JumpForce = 3f;
        }




        anim.SetFloat("Speed", Mathf.Abs(moveValue));  //sets variable in animations
        anim.SetFloat("Aim", Mathf.Abs(aimValue));  //sets variable in animations
        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);  //moves player

        if (aimValue != 0)
        {
            isUp = true;
        }
        else if (aimValue == 0)
        {
            isUp = false;
        }

        if ((moveValue > 0 && isLeft) || (moveValue < 0 && !isLeft))
        {
            flip();
        }

        if (Input.GetKeyDown(KeyCode.Q))                                  //(Input.GetButtonDown("Fire1"))  
                                                                                    //(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Bang Bang");
            fire();

        }



    }
    void OnTriggerEnter2D(Collider2D c)
    {

        Debug.Log(c.gameObject.tag);


        switch (c.gameObject.tag)
        {
            case "MegaJump":
                MegaJump = true;
                JumpForce = 6;
                break;
            case "EnemyBullet":
                PHealth = PHealth - 10;
                break;
            case "Death":
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                PHealth = 0;
                break;
            case "Coin":
                Score = Score + 10;
                break;
            case "Health":
                if (PHealth < 76)
                    PHealth = PHealth + 25;
                break;
            case "MegaPowerUp":

                break;
        }


    }


    void flip()
    {

        if (isLeft)
        {
            Debug.Log("Your Right");
            isLeft = false;
        }

        else
        {
            Debug.Log("Your Left");
            isLeft = true;
        }

        Vector3 scaleFactor = transform.localScale;

        scaleFactor.x = -scaleFactor.x;

        transform.localScale = scaleFactor;

    }
    
    void fire()
    {
        if (isLeft && !isUp)
        {
            Projectile temp =
            Instantiate(projectilePrefab,
            projectileSpawnPoint01.position,
            projectileSpawnPoint01.rotation);
            temp.speed = projectileForce;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 0);
        }
        else if (!isLeft && !isUp)
        {
            Projectile temp =
            Instantiate(projectilePrefab,
            projectileSpawnPoint01.position,
            projectileSpawnPoint01.rotation);
            temp.speed = projectileForce;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 0);
        }

        if (isLeft && isUp)
        {
            Projectile temp =
            Instantiate(projectilePrefab,
            projectileSpawnPoint02.position,
            projectileSpawnPoint02.rotation);
            temp.speed = projectileForce;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 4);
        }
        else if (!isLeft && isUp)
        {
            Projectile temp =
            Instantiate(projectilePrefab,
            projectileSpawnPoint02.position,
            projectileSpawnPoint02.rotation);
            temp.speed = projectileForce;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 4);
        }

    }
}




