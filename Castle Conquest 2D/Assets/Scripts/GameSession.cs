using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3, score = 0;
    [SerializeField] public int maxLife = 3;
    [SerializeField] TextMeshProUGUI scoreText, livesText;
    [SerializeField] private AudioClip dyingSFX;
    [SerializeField] private Image[] hearts;


    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLive();
        }

        else
        {
            AudioSource.PlayClipAtPoint(dyingSFX, Camera.main.transform.position);
            ResetGame();
        }
    }

    public void AddToScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
    private void TakeLive()
    {
        playerLives--;
        UpdateHearts();
        livesText.text = playerLives.ToString();
    }

    public void AddLive(int value)
    {
        playerLives += value;
        UpdateHearts();
        livesText.text = playerLives.ToString();

    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerLives)
            {
                hearts[i].enabled = true;
            }
            else
                hearts[i].enabled = false;

        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
