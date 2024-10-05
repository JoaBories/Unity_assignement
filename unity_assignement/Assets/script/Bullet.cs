using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]

    private float speed = 2f;
    private float maxDistance = 10f;

    public Vector3 directionVector;
    public float directionAngle;
    public GameObject player;

    bool created = false;

    void Start()
    {
        transform.eulerAngles = new Vector3(0,0,directionAngle);
    }

    void Update()
    {
        if (created)
        {
            transform.position += directionVector * speed * Time.deltaTime;
            if ((transform.position - player.transform.position).magnitude > maxDistance) destroyBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) destroyBullet();
    }

    public void launch()
    {
        created = true;
    }

    public void destroyBullet()
    {
        Destroy(gameObject);
    }
}
