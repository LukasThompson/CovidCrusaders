using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Spawnable Objects")]
    public CovidSpawnManager CSM;
    [Tooltip("Any GameObjects you wish to spawn, good or bad.")]
    public List<GameObject> goodObjects;
    public List<GameObject> badObjects;
    [Header("Spawn Points")]
    public GameObject[] spawns;

    [Header("GUI GameObjects")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    [Space]
    public Button restartButton;

    [Header("Game Options")]
    [SerializeField] public float spawnRate = 1.0f;
    public AudioManager audioManager;

    [Header("Current Game State")]
    public bool isGameActive;

    
    private int score = 0;

    public void Start()
    {
        audioManager.menuMusic.gameObject.SetActive(true);
    }
    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        spawnRate /= difficulty;
        
        audioManager.menuMusic.gameObject.SetActive(false);

        switch (difficulty)
        {
            case 1:
                audioManager.easyMusic.gameObject.SetActive(true);
                break;
            case 2:
                audioManager.mediumMusic.gameObject.SetActive(true);
                break;
            case 3:
                audioManager.hardMusic.gameObject.SetActive(true);
                break;
        }

        StartCoroutine(CSM.SpawnTarget());
        scoreText.text = "Grant $: " + score;
        UpdateScore(0);
    }


    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Grant $: " + score;
    }

    public void GameOver()
    {
        if (isGameActive)
        {
            isGameActive = false;
            TurnOffMusic();
            GameOverDetails();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TurnOffMusic()
    {
        audioManager.easyMusic.gameObject.SetActive(false);
        audioManager.mediumMusic.gameObject.SetActive(false);
        audioManager.hardMusic.gameObject.SetActive(false);
    }

    public void GameOverDetails()
    {
        audioManager.gameOverMusic.gameObject.SetActive(true);
        audioManager.announcerAudioSource.PlayOneShot(audioManager.gameOver);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
