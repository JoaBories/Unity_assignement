using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnGround : MonoBehaviour
{
    public Vector3 startingPosition;
    public bool isOnGround;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = startingPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isOnGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isOnGround = false;
    }
}
