using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject quitMenu;




    // Start is called before the first frame update
    void Start()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Settings Menu is not assigned in the Inspector!");
        }

        if (quitMenu != null)
        {
            quitMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Quit Menu is not assigned in the Inspector!");
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("1-1");
    }

    public void LoadGame()
    {
        Debug.LogWarning("Spierdalaj");
    }

    public void OpenSettings()
    {
        // Deactivate Main Menu
        gameObject.SetActive(false);
        // Activate Settings Menu
        settingsMenu.SetActive(true);

    }

    public void QuitGame()
    {
        // Deactivate Main Menu
        gameObject.SetActive(false);
        // Activate Settings Menu
        quitMenu.SetActive(true);

    }




}
