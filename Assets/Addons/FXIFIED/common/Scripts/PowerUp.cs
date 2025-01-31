using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PowerUp : MonoBehaviour
{

    public GameObject pickupEffect;

    public TextMeshProUGUI countText;
    //public GameObject winTextObject;
    public static int count = 0;

    private void Start()
    {
        // Obtén la referencia al componente Text
        countText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        SetCountText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        count = count + 1;

        // Run the 'SetCountText()' function (see below)
        SetCountText();

        Destroy(gameObject);
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();

    }
}
