using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour, IHealth
{

    public float maxHealth = 100f;
    public float recoveryRate = 0.1f;
    public float startingHealth = 100f; // The amount of health the player starts the game with.
    public float currentHealth; // The current health the player has.
    public Slider healthSlider; // Reference to the UI's health bar.
    public Image damageImage; // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip; // The audio clip to play when the player dies.
    public float flashSpeed = 5f; // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); // The colour the damageImage is set to, to flash.
    public GameObject gameOverText;

    Animator anim; // Reference to the Animator component.
    AudioSource playerAudio; // Reference to the AudioSource component.
    PlayerMovement playerMovement; // Reference to the player's movement.
    PlayerShooting playerShooting; // Reference to the PlayerShooting script.
    bool isDead; // Whether the player is dead.
    bool damaged; // True when the player gets damaged.

    private void Start()
    {
        currentHealth = maxHealth;
        gameOverText.SetActive(false);
    }

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth; // Set the initial health of the player.
    }
    void Update()
    {
        if (damaged) // If the player has just been damaged...
        {
            damageImage.color = flashColour; // ... set the colour of the damageImage to the flash colour.

        }
        else // Otherwise...
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // ... transition the colour back to clear.
        }
        damaged = false; // Reset the damaged flag.
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    //public float MaxHealth { get => m_StartingHealth; }

    bool IHealth.IsHit
    {
        get => damaged;
    }

    float IHealth.MaxHealth
    {
        get => startingHealth;
    }
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        damaged = true; // Set the damaged flag so the screen will flash.
        currentHealth -= amount; // Reduce the current health by the damage amount.
        healthSlider.value = currentHealth; // Set the health bar's value to the current health.
        playerAudio.Play(); // Play the hurt sound effect.
        if (currentHealth <= 0 && !isDead) // If the player has lost all it's health and the death flag hasn't been set yet...
        {
            Death(); // ... it should die.
        }
    }

    public void AddHealth(int amount)
    {
        if (currentHealth < 100) 
        { 
        currentHealth += amount;
        healthSlider.value = currentHealth;
        }
    }

    void Death()
    {
        isDead = true; // Set the death flag so this function won't be called again.
        playerShooting.DisableEffects(); // Turn off any remaining shooting effects.
        anim.SetTrigger("Die"); // Tell the animator that the player is dead.
        playerAudio.clip = deathClip; // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.Play();
        playerMovement.enabled = false; // Turn off the movement scripts.
        playerShooting.enabled = false; // Turn off the shooting scripts.
        gameOverText.SetActive(true); // Turn on the game over text.
    }
}
