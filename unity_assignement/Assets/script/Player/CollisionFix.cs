using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFix : MonoBehaviour
{
    public bool collisionState = false;
    public Vector3 startingPosition;

    void Update()
    {
        transform.localPosition = startingPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) collisionState = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) collisionState = false;
    }
}
