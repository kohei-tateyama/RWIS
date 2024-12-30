using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // public bool pauseMenuIsOn { get; private set; }
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject quitMenu;

    

    void Start()
    {
        if (settingsMenu != null)
        {
            // pauseMenuIsOn = false;
        }
        else
        {
            Debug.LogWarning("Settings Menu is not assigned in the Inspector!");
        }

        if (quitMenu != null)
        {
        }
        else
        {
            Debug.LogWarning("Quit Menu is not assigned in the Inspector!");
        }
    }

    void OnEnable()
    {
        settingsMenu.SetActive(false);
        quitMenu.SetActive(false);
    }

    public void QuickSave()
    {
        Debug.Log("Quick Save is not implemented");
    }

    public void Return2Game()
    {
        PauseMenuManager.GetInstance().ExitPauseMenu();
    }

    public void LoadGame()
    {
        Debug.Log("Load Game is not implemented");
    }

    public void OpenSettingsMenu()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void OpenQuitMenu()
    {
        gameObject.SetActive(false);
        quitMenu.SetActive(true);
    }
    
}
