using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip heartPickUpSFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(heartPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
