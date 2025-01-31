using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour, IHealth
{
    public float startingHealth = 100f;            // The amount of health the enemy starts the game with.
    public float currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.
    public bool isHit;

    Animator anim;                              // Reference to the animator.
    ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    bool isSinking;                             // Whether the enemy has started sinking through the floor.

    [SerializeField]
    GameObject afterDeathSpawn;

    [SerializeField]
    GameObject afterDeathSpawn2;

    public Slider healthSlider;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    //public float MaxHealth { get => m_StartingHealth; }

    bool IHealth.IsHit
    {
        get => isHit;
    }
    float IHealth.MaxHealth
    {
        get => startingHealth;
    }
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;
        healthSlider.value = currentHealth; // Set the health bar's value to the current health.

        // Set the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

        // And play the particles.
        hitParticles.Play();
        GameObject spawnInstance2 = Instantiate(afterDeathSpawn2, transform.position, Quaternion.identity);
        Destroy(spawnInstance2, 1f);

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
            healthSlider.value = 0; // Set the health bar's value to the current health.
        }
    }


    void Death()
    {
        // El enemigo está muerto.
        isDead = true;

        // Instanciar afterDeathSpawn y afterDeathSpawn2
        GameObject spawnInstance1 = Instantiate(afterDeathSpawn, transform.position, Quaternion.identity);
        GameObject spawnInstance2 = Instantiate(afterDeathSpawn2, transform.position, Quaternion.identity);

        // Destruir afterDeathSpawn2 después de 5 segundos, por ejemplo
        Destroy(spawnInstance1, 8f);
        Destroy(spawnInstance2, 1f);

        // Convertir el collider en un trigger para que las balas lo atraviesen.
        capsuleCollider.isTrigger = true;

        // Informar al animator que el enemigo ha muerto.
        anim.SetTrigger("Dead");
    }


    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
        ScoreManager.score += scoreValue;

       

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}
