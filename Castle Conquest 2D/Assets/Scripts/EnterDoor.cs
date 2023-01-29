using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] private AudioClip openningDoorSFX;
    [SerializeField] private AudioClip closingDoorSFX;


    void Start()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<Animator>().SetTrigger("Close");
    }
    void PlayOpeningSFX()
    {
        AudioSource.PlayClipAtPoint(openningDoorSFX, Camera.main.transform.position);
    }

    void PlayClosingSFX()
    {
        AudioSource.PlayClipAtPoint(closingDoorSFX, Camera.main.transform.position);
    }
}
