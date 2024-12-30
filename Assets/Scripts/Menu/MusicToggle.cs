using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    private MusicManager musicManager; // Reference to the singleton MusicManager
    private Toggle musicToggle;        // Reference to the toggle component

    void Awake()
    {
        musicManager = MusicManager.GetInstance();
        musicToggle = GetComponent<Toggle>();

        if (musicManager == null)
        {
            Debug.LogError("MusicManager instance is null in MusicToggle.");
        }

        if (musicToggle == null)
        {
            Debug.LogError("Toggle component is missing.");
        }
    }

    void Start()
    {
        if (musicManager != null && musicToggle != null)
        {
            // Initialize toggle state based on the AudioSource mute state
            musicToggle.isOn = musicManager.IsMusicOn();

            // Add listener to handle toggle value changes
            musicToggle.onValueChanged.AddListener(isOn =>
            {
                musicManager.SetMusicState(isOn);
            });

            Debug.Log($"MusicToggle initialized. Is On: {musicToggle.isOn}");
        }
    }

    void OnDestroy()
    {
        // Clean up listeners to avoid memory leaks
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.RemoveAllListeners();
        }
    }
}
