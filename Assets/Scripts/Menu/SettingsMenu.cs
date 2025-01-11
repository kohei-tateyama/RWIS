using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject parentMenu;
    [SerializeField] AudioMixer audioMixer;

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

        if (audioMixer == null)
        {
            Debug.LogWarning("Settings Menu: Audio Mixer is not assigned in the Inspector");
        }
    }

    public void OnDropdownValueChangedChooseFilter(int index)
    {
        AudioMixerSnapshot snapshot;
        switch (index)
        {
            case 0:
                snapshot = audioMixer.FindSnapshot("FilterOnEverything");
                break;
            case 1:
                snapshot = audioMixer.FindSnapshot("DialoguesFilteredOut");
                break;
            case 2:
                snapshot = audioMixer.FindSnapshot("MusicFilteredOut");
                break;
            case 3:
                snapshot = audioMixer.FindSnapshot("NoFilter");
                break;

            default:
                snapshot = audioMixer.FindSnapshot("NoFilter");
                break;
        }
        snapshot.TransitionTo(0);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
