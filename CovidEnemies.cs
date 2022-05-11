using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovidEnemies : MonoBehaviour
{
    [Header("Enemy Properties")]
    public int pointValue;
    public ParticleSystem[] explosionParticles;

    private GameManager gameManager;
    private AudioManager audioManager;
    private AudioSource audioSource;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        audioSource = FindObjectOfType<DeathAudio>().GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (gameManager.isGameActive)
            {
                KillEnemy();
                Destroy(other.gameObject);               
            }
        }

        if (other.gameObject.CompareTag("Sensor"))
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(),
                collision.gameObject.GetComponent<Collider>());
        }

        if (collision.gameObject.CompareTag("Powerup"))
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(),
                collision.gameObject.GetComponent<Collider>());
        }
    }

    private void KillEnemy()
    {
        int audioIndex = Random.Range(0, audioManager.covidDeaths.Length);
        int index = Random.Range(0, explosionParticles.Length);

        // Play Death Sounds
        audioSource.PlayOneShot(audioManager.covidDeaths[audioIndex]);
        
        // Play Death Animation

        // Destroy GameObject after audio is played
        Destroy(gameObject);

        // Spawn Death VFX 
        Instantiate(explosionParticles[index],
            transform.position, explosionParticles[index].transform.rotation);

        // Add Score
        gameManager.UpdateScore(pointValue);
    }
}
