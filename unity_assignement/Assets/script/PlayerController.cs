using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public int jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            GetComponent<Animator>().SetBool("walking", true);
        }

        if (Input.GetKey(KeyCode.A))
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
    }
}
