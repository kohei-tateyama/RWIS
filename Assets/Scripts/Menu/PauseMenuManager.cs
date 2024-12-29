using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject touchInputPanel;
    public bool isPauseMenuOn { get; private set; }

    private static PauseMenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than on Pause Menu Manager in the scene");
        }
        instance = this;
    }

    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("Pause Menu is not included in the inspector");
        }
    }
    
    void Update()
    {
        if (InputManager.GetInstance().GetPausePressed())
        {
            isPauseMenuOn = true;
            pauseMenu.SetActive(true);
            touchInputPanel.SetActive(false);
        
        }
    }

    public static PauseMenuManager GetInstance()
    {
        return instance;
    }


    public void ExitPauseMenu() 
    {
        isPauseMenuOn = false;
        pauseMenu.SetActive(false);
        touchInputPanel.SetActive(true);
    }

}
