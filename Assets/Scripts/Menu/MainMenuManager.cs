using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject quitMenu;
    [SerializeField] private GameObject newGameMenu;




    // Start is called before the first frame update
    void Start()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Settings Menu is not assigned in the Inspector!");
        }

        if (quitMenu != null)
        {
            quitMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Quit Menu is not assigned in the Inspector!");
        }

        if (newGameMenu != null)
        {
            newGameMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("New Game Menu is not assigned in the Inspector!");
        }

    }

    public void PlayNewGame()
    {
        //TODO: if no save data, just load game
        newGameMenu.SetActive(true);
        gameObject.SetActive(false);
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
