using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip diamondPickUpSFX;
    [SerializeField] private int diamondValue = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(diamondPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(diamondValue);
    }
}
