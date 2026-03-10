using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void MainMenuTransfer()
    {
        PauseMenu.IsPaused = false;
        SceneManager.LoadScene("PressPlayToStart");
    }

}
