using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeStudioNameScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start timed event
        StartCoroutine(ChangeSceneCoroutine());
    }

    // Wait certain amount of time before executing
    IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(4f);
        // Executes code here
        SceneManager.LoadScene("PressPlayToStart");
    }
}
