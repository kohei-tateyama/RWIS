using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject quitMenu;

    void Start()
    {
        if (settingsMenu != null)
        {
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
        gameObject.SetActive(true);
        settingsMenu.SetActive(false);
        quitMenu.SetActive(false);
    }

    public void QuickSave()
    {
        Debug.Log("Quick Save is not implemented");
    }

    public void Return2Game()
    {
        gameObject.SetActive(false);
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

    }
    
    public void OpenQuitMenu()
    {
        gameObject.SetActive(false);
        quitMenu.SetActive(true);
    }


    private void DisableAllChildren()
    {
        foreach (Transform child in gameObject.transform)
        {
            // Disable each child in Parent Object
            child.gameObject.SetActive(false);
        }
    }

    private void ActivateAllChildren()
    {
        foreach (Transform child in gameObject.transform)
        {
            // Disable each child in Parent Object
            child.gameObject.SetActive(true);
        }
    }

}
