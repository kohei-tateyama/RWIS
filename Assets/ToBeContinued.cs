using UnityEngine;

public class ToBeContinued : MonoBehaviour
{

    // Delay time in seconds before quitting
    public float delayTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Call the QuitGame method after the specified delay
        Invoke("QuitGame", delayTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void QuitGame()
    {
        // If running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a standalone build
        Application.Quit();
#endif
    }
}