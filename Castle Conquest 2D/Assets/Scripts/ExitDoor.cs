using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private float secondsToLoad = 2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Animator>().SetTrigger("Open");
    }
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    GetComponent<Animator>().SetTrigger("Close");
    //}

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
}
