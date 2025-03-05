using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TouchController : MonoBehaviour
{
    public int gold = 0;
    public TextMeshProUGUI goldText;

    private void Start()
    {
        goldText.SetText(gold.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gold"))
        {
            gold++;
            goldText.SetText(gold.ToString());
            Destroy(collision.gameObject);
        }
    }
}
