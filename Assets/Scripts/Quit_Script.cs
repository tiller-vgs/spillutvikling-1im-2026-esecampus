using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit_Script : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}
