using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{


    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetInstance().GetPausePressed())
        {
            Debug.LogWarning("Pause Menu not integrated");
        }
    }
}
