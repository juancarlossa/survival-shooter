using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRec : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private float recoveryRate = 0.05f; // Tasa de recuperaci�n por segundo
    private float recoveryInterval = 0.3f;
    public Slider healthSlider; // Reference to the UI's health bar.
    private bool isPlayerInside = false;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        //healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            // Contin�a la recuperaci�n de salud
            StartCoroutine(RecoverHealthOverTime());
            healthSlider.value = playerHealth.currentHealth;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private System.Collections.IEnumerator RecoverHealthOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < recoveryInterval)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        while (isPlayerInside && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            yield return new WaitForSeconds(recoveryInterval);
            playerHealth.currentHealth += (recoveryRate);
            healthSlider.value = playerHealth.currentHealth;

            // Aseg�rate de no exceder la salud m�xima
            if (playerHealth.currentHealth > playerHealth.maxHealth)
            {
                playerHealth.currentHealth = playerHealth.maxHealth;
                healthSlider.value = playerHealth.currentHealth;
            }
        }
    }
}
