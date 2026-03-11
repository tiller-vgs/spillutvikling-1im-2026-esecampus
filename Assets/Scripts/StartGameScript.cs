using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadGameScene()
    {

        
        Debug.Log("fweh");
        SceneManager.LoadScene("Level1");

    }
}