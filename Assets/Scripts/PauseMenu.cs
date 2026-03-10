using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject Pause_Menu;


    public static bool IsPaused;



    private void Start()
    {
        Pause_Menu.SetActive(false);
        IsPaused = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void PauseGame()
    {
               Time.timeScale = 0f;
        Pause_Menu.SetActive(true);
        IsPaused = true;

        }

    public void ResumeGame()
    {
               Time.timeScale = 1f;
        Pause_Menu.SetActive(false);
        IsPaused = false;

    }
}
