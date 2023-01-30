using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private float secondsToLoad = 2f;
    [SerializeField] private AudioClip openningDoorSFX;
    [SerializeField] private AudioClip closingDoorSFX;
    [SerializeField] private TextMeshProUGUI helpText;

     void Start()
    {
        helpText.enabled=false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Animator>().SetTrigger("Open");
        helpText.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //GetComponent<Animator>().SetTrigger("Close");
        helpText.enabled = false;

    }

    public void StartLoadingNextLevel()
    {
        GetComponent<Animator>().SetTrigger("Close");
        FindObjectOfType<Player>().isLoading = true;
        StartCoroutine(LoadNextLevel());

    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(secondsToLoad);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
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
