using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitParticles;

    [SerializeField] bool applyCameraShake;
    [SerializeField] bool useHealthPoints;
    CameraShake cameraShake;
    AudioManager audioManager;

    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            PlayHitParticles();
            ScoreManager.FindAnyObjectByType<ScoreManager>().SubtractScore(10);
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
            audioManager.PlayDamageSFX();

            if (applyCameraShake && cameraShake != null)
            {
                cameraShake.Play();
            }

            if (useHealthPoints)
            {
                HealthManager.FindAnyObjectByType<HealthManager>().SubtractHealth(damageDealer.GetDamage());
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (health <= 0 && useHealthPoints)
        {
            Destroy(gameObject);
            WaitForSeconds wait = new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void PlayHitParticles()
    {
        if (hitParticles != null)
        {
            ParticleSystem particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
