using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu()
    {
        var gameSession = FindObjectOfType<GameSession>();
        ResetInformation(gameSession);
        SceneManager.LoadScene(0);
    }

    private void ResetInformation(GameSession gameSession)
    {
        gameSession.playerLives = gameSession.maxLife;
        gameSession.UpdateHearts();
        gameSession.livesText.text = gameSession.playerLives.ToString();
        gameSession.score = 0;
        gameSession.scoreText.text = "0";
    }
}
