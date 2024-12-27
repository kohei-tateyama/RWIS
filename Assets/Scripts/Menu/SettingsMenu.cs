using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject parentMenu;

    // Set Settigs menu disabled, activate Main menu
    public void GoBack()
    {
        // Deactivate Settings
        gameObject.SetActive(false);
        // Activate Main Menu
        parentMenu.SetActive(true);
    
    }
    // Start is called before the first frame update
    void Start()
    {
        if (parentMenu != null)
        {
        }
        else
        {
            Debug.LogWarning("parentMenu is not assigned in the Inspector!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
