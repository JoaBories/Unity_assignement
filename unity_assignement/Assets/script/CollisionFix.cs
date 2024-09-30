using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFix : MonoBehaviour
{
    public bool collisionState = false;
    public Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = startingPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionState = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionState = false;
    }

}
