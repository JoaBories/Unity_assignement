using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Collectibles : MonoBehaviour
{
    public int coin = 0;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            collision.gameObject.SetActive(false);
            coin++;
        }
    }
}
