using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject MenuUI;
    public GameObject OptionsUI;

    public Button buttonResume;

    public bool GameStart = false;
    public bool GameIsPaused = false;
    public bool GameOptions = false;

    private void Start()
    {
        buttonResume.interactable = false;

        PauseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (GameIsPaused)
                Resume();
            else
                PauseMenu();

        //open options menu
        if (GameOptions)
            Options();

        //player is dead
        if (FindObjectOfType<GameManager>().playerIsDead)
        {
            buttonResume.interactable = false;

            PauseMenu();
        }

        //hide cursor
        if (GameStart)
            Cursor.visible = false;

        //show cursor
        if (GameIsPaused)
            Cursor.visible = true;
    }

    public void PauseMenu()
    {
        if (!GameOptions)
        {
            GameUI.SetActive(false);
            MenuUI.SetActive(true);

            Time.timeScale = 0.0f;

            GameStart = false;
            GameIsPaused = true;
        }
    }

    public void Resume()
    {
        GameUI.SetActive(true);
        MenuUI.SetActive(false);

        Time.timeScale = 1.0f;

        GameStart = true;
        GameIsPaused = false;
    }


    public void NewGame()
    {
        //Respawn player
        FindObjectOfType<GameManager>().Respawn();

        //set game to normal speed
        Time.timeScale = 1.0f;

        //set game conditions
        GameStart = true;
        GameIsPaused = false;

        //set UI active/non-active
        MenuUI.SetActive(false);
        GameUI.SetActive(true);

        //enable Resume button
        buttonResume.interactable = true;
    }

    public void Options()
    {
        GameOptions = true;

        GameStart = false;
        GameIsPaused = true;

        Time.timeScale = 0.0f;

        GameUI.SetActive(false);
        MenuUI.SetActive(false);
        OptionsUI.SetActive(true);
    }

    public void Back()
    {
        //save changes in settings class

        GameOptions = false;

        GameStart = false;
        GameIsPaused = true;

        Time.timeScale = 0.0f;

        GameUI.SetActive(false);
        MenuUI.SetActive(true);
        OptionsUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
