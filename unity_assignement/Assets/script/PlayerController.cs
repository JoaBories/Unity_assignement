using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject collisionFixLeft;
    public GameObject collisionFixRight;
    public GameObject isOnGround;
    public GameObject spawnPoint;

    [SerializeField] bool canDoubleJump = true;
    [SerializeField] bool ground;
    [SerializeField] bool collisionLeft;
    [SerializeField] bool collisionRight;
    [SerializeField] float canMoveTimer;

    private void Start()
    {
        transform.position = spawnPoint.transform.position;
        canMoveTimer = 1;
    }
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();

        ground = isOnFloor();
        collisionRight = collisionFixRight.GetComponent<CollisionFix>().collisionState;
        collisionLeft = collisionFixLeft.GetComponent<CollisionFix>().collisionState;

        if (canMoveTimer == 0)
        {
            //move
            if (Input.GetKey(KeyCode.D) && !collisionRight)
            {
                spriteRenderer.flipX = false;
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                animator.SetBool("walking", true);
            }
            if (Input.GetKey(KeyCode.A) && !collisionLeft)
            {
                spriteRenderer.flipX = true;
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                animator.SetBool("walking", true);
            }
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                animator.SetBool("walking", false);
            }


            //jump
            if (Input.GetKeyDown(KeyCode.Space) && ground)
            {
                rigidBody2D.velocity = new Vector2(0, jumpForce);
            }
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !ground)
            {
                rigidBody2D.velocity = new Vector2(0, jumpForce);
                canDoubleJump = false;
            }

            if (!ground)
            {
                animator.SetBool("air", true);
                if (rigidBody2D.velocity.y < 0) animator.SetBool("up", false);
                else animator.SetBool("up", true);
            }
            else animator.SetBool("air", false);

            if (ground) canDoubleJump = true;

            if (canDoubleJump) spriteRenderer.color = Color.green;
            else spriteRenderer.color = Color.yellow;
        }

        //restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.Play("die");
            canMoveTimer = 1.2f;
        }

        //Timer
        canMoveTimer -= Time.deltaTime;
        if (canMoveTimer < 0) canMoveTimer = 0;

    }

    bool isOnFloor()
    {
        return isOnGround.GetComponent<IsOnGround>().isOnGround;
    }

    void die(Animator animator)
    {
        transform.position = spawnPoint.transform.position;
        canMoveTimer = 1;
        animator.Play("spawn");
    }

}
