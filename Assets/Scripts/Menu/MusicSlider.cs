using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private MusicManager musicManager; // Reference to the singleton MusicManager
    private Slider volumeSlider;       // Reference to the slider component

    void Awake()
    {
        musicManager = MusicManager.GetInstance();
        volumeSlider = GetComponent<Slider>();

        if (musicManager == null)
        {
            Debug.LogError("MusicManager instance is null in MusicSlider.");
        }

        if (volumeSlider == null)
        {
            Debug.LogError("Slider component is missing.");
        }
    }

    void Start()
    {
        if (musicManager != null && volumeSlider != null)
        {
            // Initialize the slider with the current volume from MusicManager
            volumeSlider.value = musicManager.GetVolume();

            // Add listener to handle slider value changes
            volumeSlider.onValueChanged.AddListener(value =>
            {
                musicManager.UpdateVolume(value);
            });

            Debug.Log($"MusicSlider initialized with value: {volumeSlider.value}");
        }
    }

    void OnDestroy()
    {
        // Clean up listeners to avoid memory leaks
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveAllListeners();
        }
    }
}
