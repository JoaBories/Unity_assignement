using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    //public GameObject collisionFix;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) /*&& !collisionFix.GetComponent<CollisionFix>().collisionState*/)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            GetComponent<Animator>().SetBool("walking", true);
        }

        if (Input.GetKey(KeyCode.A) /*&& !collisionFix.GetComponent<CollisionFix>().collisionState*/)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            GetComponent<Animator>().SetBool("walking", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            GetComponent<Rigidbody2D>().velocity += new Vector2(0,jumpForce);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("walking", false);
        }

        if (GetComponent<Rigidbody2D>().velocity.y > 0.5f || GetComponent<Rigidbody2D>().velocity.y < -0.5f)
        {
            GetComponent<Animator>().SetBool("air", true);
            if (GetComponent<Rigidbody2D>().velocity.y < 0) GetComponent<Animator>().SetBool("up", false);
            else GetComponent<Animator>().SetBool("up", true);
        }
        else GetComponent<Animator>().SetBool("air", false); 
    }
}
