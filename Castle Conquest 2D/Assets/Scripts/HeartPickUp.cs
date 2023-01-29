using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip heartPickUpSFX;
    [SerializeField] private int heartValue = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var gameSession = FindObjectOfType<GameSession>();
        var maxLife = gameSession.maxLife;
        var life = gameSession.playerLives;

        if (life + heartValue <= maxLife)
        {
            AudioSource.PlayClipAtPoint(heartPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
            gameSession.AddLive(heartValue);
        }
    }
}
