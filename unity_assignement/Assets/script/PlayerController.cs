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
    [SerializeField] bool canDoubleJump = true;

    [SerializeField] bool ground;
    [SerializeField] bool collisionLeft;
    [SerializeField] bool collisionRight;

    void Update()
    {
        ground = isOnFloor();
        collisionRight = collisionFixRight.GetComponent<CollisionFix>().collisionState;
        collisionLeft = collisionFixLeft.GetComponent<CollisionFix>().collisionState;

        if (Input.GetKey(KeyCode.D) && !collisionRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            GetComponent<Animator>().SetBool("walking", true);
        }

        if (Input.GetKey(KeyCode.A) && !collisionLeft)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            GetComponent<Animator>().SetBool("walking", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ground)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !ground)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
            canDoubleJump = false;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("walking", false);
        }

        if (!ground)
        {
            GetComponent<Animator>().SetBool("air", true);
            if (GetComponent<Rigidbody2D>().velocity.y < 0) GetComponent<Animator>().SetBool("up", false);
            else GetComponent<Animator>().SetBool("up", true);
        }
        else  GetComponent<Animator>().SetBool("air", false);

        if (ground) canDoubleJump = true;

        if (canDoubleJump) GetComponent<SpriteRenderer>().color = Color.green;
        else GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    bool isOnFloor()
    {
        return isOnGround.GetComponent<IsOnGround>().isOnGround;
    }
}
