using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsZone : MonoBehaviour
{
    public GameObject TipsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) TipsText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) TipsText.SetActive(false);
    }


}
