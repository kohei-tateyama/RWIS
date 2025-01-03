using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;
    public int coins { get; private set; } = 0;

    public Vector2 previousPlayerPosition;
    public string nextDoorID;

    private void Awake()
    {
        if (Instance != null) 
        {
            DestroyImmediate(gameObject);
        } 
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            // Unsubscribe from the sceneLoaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;

            Instance = null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("loaded scene name: " + scene.name);
        
        // Find the player in the scene
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            if (!string.IsNullOrEmpty(nextDoorID))
            {
                // Find the door with the matching doorID
                Door targetDoor = FindDoorByID(nextDoorID);
                

                if (targetDoor != null)
                {
                    Debug.Log("target door name: " + targetDoor.name);
                    // Set the player's position to the target door's position
                    player.transform.position = targetDoor.transform.position;
                }
                else
                {
                    Debug.LogWarning("Target door not found: " + nextDoorID);
                }

                // Reset nextDoorID after use
                nextDoorID = null;
            }
            else if (previousPlayerPosition != new Vector2(2, 2))
            {
                // Return the player to the previous position
                player.transform.position = previousPlayerPosition;

                // Reset previousPlayerPosition
                previousPlayerPosition = new Vector2(2, 2);
            }
            else
            {
                // Default spawn position
                player.transform.position = new Vector2(2, 2);
            }
        }
        else
        {
            Debug.LogWarning("Player not found in the scene.");
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        NewGame();
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void LoadRoom(string room)
    {
        SceneManager.LoadScene(room);
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    public void AddCoin()
    {
        coins++;

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        lives++;
    }

    // Helper method to find a door by its ID
    private Door FindDoorByID(string doorID)
    {
        Door[] doors = FindObjectsOfType<Door>();
        foreach (Door door in doors)
        {
            if (door.doorID == doorID)
            {
                return door;
            }
        }
        return null;
    }

}
