using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Camera mainCamera;

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;

    private Vector2 playerSpawnPosition = new Vector2(0, 3);


    private void Awake()
    {
        mainCamera = Camera.main;

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            
            if (mainCamera != null)
            {
                mainCamera.fieldOfView = 20f;
            }
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (mainCamera != null)
            {
                mainCamera.fieldOfView = 200f;
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method will be called after a new scene has been loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set the player's position
        StartCoroutine(SetPlayerPositionCoroutine(playerSpawnPosition));
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Spaceport");
        this.playerSpawnPosition = new Vector2(0, -1.05f);
    }

    public void GameOver()
    {
        NewGame();
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("ToBeContinued");
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

    public void LoadRoom(string room, Vector2 playerSpawnPosition)
    {
        SceneManager.LoadScene(room);
        this.playerSpawnPosition = playerSpawnPosition;
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

    private IEnumerator SetPlayerPositionCoroutine(Vector2 newPosition)
    {
        // Wait until the end of the frame to ensure all objects are initialized
        yield return new WaitForEndOfFrame();

        // Now proceed to set player position and move camera
        SetPlayerPosition(newPosition);
    }

    private void SetPlayerPosition(Vector2 newPosition)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }

        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            SideScrollingCamera sideScrolling = mainCamera.GetComponent<SideScrollingCamera>();
            if (sideScrolling != null)
            {
                sideScrolling.MoveCamera(newPosition);
            }
            else
            {
                Debug.LogWarning("Camera-SideScrolling script not found.");
            }
        }
    }

}
