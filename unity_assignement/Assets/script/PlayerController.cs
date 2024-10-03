using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float dashForce;
    
    public Vector2 dashVector;
    public bool cantMove;


    [Header("Game Object Assign")]
    public GameObject collisionFixLeft;
    public GameObject collisionFixRight;
    public GameObject isOnGround;
    public GameObject spawnPoint;

    bool canDoubleJump;
    bool ground;
    bool collisionLeft;
    bool collisionRight;
    
    Vector2 oldVelocity;

    private void Start()
    {
        transform.position = spawnPoint.transform.position;
    }
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
        Light2D spotLight = GetComponent<Light2D>();

        collisionRight = collisionFixRight.GetComponent<CollisionFix>().collisionState;
        collisionLeft = collisionFixLeft.GetComponent<CollisionFix>().collisionState;
        
        ground = isOnFloor();
        oldVelocity = rigidBody2D.velocity;


        if (!cantMove)
        {
            //crouch
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.localScale = new Vector3(2, 1.5f, 2);
                transform.position += new Vector3(0, -0.05f, 0);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                transform.localScale = new Vector3(2, 2, 2);
                transform.position += new Vector3(0, 0.05f, 0);
            }


            //move
            if (Input.GetKey(KeyCode.D) && !collisionRight)
            {
                spriteRenderer.flipX = false;
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                if (oldVelocity.x < 0)
                {
                    rigidBody2D.velocity = new Vector2(0, oldVelocity.y);
                }
                animator.SetBool("walking", true);
            }
            if (Input.GetKey(KeyCode.A) && !collisionLeft)
            {
                spriteRenderer.flipX = true;
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (oldVelocity.x > 0)
                {
                    rigidBody2D.velocity = new Vector2(0, oldVelocity.y);
                }
                animator.SetBool("walking", true);
            }
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                animator.SetBool("walking", false);
            }


            //Dash
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.D)) dashVector = new Vector2(1.5f, 2);
                else if (Input.GetKey(KeyCode.A)) dashVector = new Vector2(-1.5f, 2);
                else if (Input.GetKey(KeyCode.S)) dashVector = new Vector2(0, 0);
                else dashVector = new Vector2(0, 3);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.D)) dashVector = new Vector2(1.5f, -2);
                else if (Input.GetKey(KeyCode.A)) dashVector = new Vector2(-1.5f, -2);
                else if (Input.GetKey(KeyCode.W)) dashVector = new Vector2(0, 0);
                else dashVector = new Vector2(0, -3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A)) dashVector = new Vector2(0, 0);
                else dashVector = new Vector2(3, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.D)) dashVector = new Vector2(0, 0);
                else dashVector = new Vector2(-3, 0);
            }

            if (Input.GetKeyDown(KeyCode.L) && canDoubleJump)
            {
                rigidBody2D.velocity = dashVector;
                canDoubleJump = false;
                animator.Play("dash");
            }


            //jump
            if (Input.GetKeyDown(KeyCode.K) && ground)
            {
                rigidBody2D.velocity = new Vector2(oldVelocity.x , jumpForce);
            }


            if (!ground)
            {
                animator.SetBool("air", true);
                if (rigidBody2D.velocity.y < 0) animator.SetBool("up", false);
                else animator.SetBool("up", true);
            }
            else animator.SetBool("air", false);

            if (ground) canDoubleJump = true;

            if(canDoubleJump)
            {
                spriteRenderer.color = Color.green;
                spotLight.color = Color.green;
            }
            else
            {
                spriteRenderer.color = Color.yellow;
                spotLight.color = Color.yellow;
            }
        }

        //restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.Play("die");
        }

        //Timer

    }

    bool isOnFloor()
    {
        return isOnGround.GetComponent<IsOnGround>().isOnGround;
    }

    void die()
    {
        transform.position = spawnPoint.transform.position;
        GetComponent<Animator>().Play("spawn");
    }

    void dashVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = dashVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Animator animator = GetComponent<Animator>();

        if (collision.gameObject.CompareTag("Damage"))
        {
            animator.Play("die");
        }
    }
}
