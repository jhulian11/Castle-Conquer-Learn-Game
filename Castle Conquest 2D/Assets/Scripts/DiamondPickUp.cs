using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip diamondPickUpSFX;
    [SerializeField] private int diamondValue = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() == typeof(BoxCollider2D))
        {
            AudioSource.PlayClipAtPoint(diamondPickUpSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(diamondValue);
            Destroy(gameObject);
        }
      
    }
}
