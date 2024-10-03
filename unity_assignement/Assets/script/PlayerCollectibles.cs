using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    public int coin = 0;
    public Text coinCounter;

    void Update()
    {
        coinCounter.text = ": " + coin;
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
