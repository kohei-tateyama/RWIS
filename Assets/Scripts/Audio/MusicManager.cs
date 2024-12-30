using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource; // The AudioSource responsible for playing music
    private static MusicManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        // Ensure if Audio Source Exists. Basically if there is no Audio Source, create an new one.
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false; // Ensure it doesn't play automatically
            Debug.Log("AudioSource was missing. A new AudioSource has been created.");
        }
    }

    public void UpdateVolume(float newVolume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp01(newVolume);
            Debug.Log($"Music volume updated to: {musicSource.volume}");
        }
        else
        {
            Debug.LogWarning("UpdateVolume called, but no AudioSource is assigned.");
        }
    }

    public void SetMusicState(bool isOn)
    {
        if (musicSource != null)
        {
            if (isOn)
            {
                if (!musicSource.isPlaying)
                {
                    musicSource.Play(); // Replay the music if it was stopped
                    Debug.Log("Music replayed from the beginning.");
                }
                musicSource.mute = false;
                Debug.Log("Music turned ON.");
            }
            else
            {
                musicSource.Stop();
                Debug.Log("Music stopped.");
            }
        }
        else
        {
            Debug.LogWarning("SetMusicState called, but no AudioSource is assigned.");
        }
    }

    public float GetVolume()
    {
        if (musicSource != null)
        {
            return musicSource.volume;
        }

        Debug.LogWarning("GetVolume called, but no AudioSource is assigned. Returning default value of 0.");
        return 0f; // Default value
    }

    public bool IsMusicOn()
    {
        if (musicSource != null)
        {
            return !musicSource.mute;
        }
        
        Debug.LogWarning("IsMusicOn called, but no AudioSource is assigned. Returning false.");
        return false; // Default state
    }

    public static MusicManager GetInstance()
    {
        if (instance == null)
        {
            // Attempt to create a new instance dynamically if one doesn't exist
            GameObject obj = new GameObject("MusicManager");
            instance = obj.AddComponent<MusicManager>();
            Debug.LogWarning("MusicManager was missing and has been dynamically created.");
        }
        return instance;
    }
}

