using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject Pause_Menu;


    public static bool IsPaused;


    private void OnEnable()
    {
        ResumeGame();
    }

    private void Start()
    {
        Cursor.visible = false;
        Pause_Menu.SetActive(false);
        IsPaused = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                PauseGame();
            }
            else
            {
                Debug.Log("Vi tok bort vlaget til å trykke esc for å komme seg ut fra pausemeny");
            }
        }
    }


    public void PauseGame()
    {
               Time.timeScale = 0f;
        Pause_Menu.SetActive(true);
        IsPaused = true;
        Cursor.visible = true;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Pause_Menu.SetActive(false);
        IsPaused = false;
        Cursor.visible = false;
        Debug.Log("Resumer spillet");

    }
}
