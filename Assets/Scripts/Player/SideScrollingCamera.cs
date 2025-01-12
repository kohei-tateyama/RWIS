using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrollingCamera : MonoBehaviour
{
    [Header("Tracked Object")]
    [SerializeField] private Transform trackedObject;

    [Header("Scene")]
    [SerializeField] private string scene;
    
    private float hallwayHeight = 4.5f;
    private float hallwayMinX = -40f;
    private float hallwayMaxX = 60f;

    private float classroomHeight = -20f;
    public float classroomThreshold = -10f;
    private float classroomMinX = -40;
    private float classroomMaxX = 30;

    private float teachersRoomHeight = 20f;
    public float teachersRoomThreshold = 10f;
    private float teachersRoomMinX = 9.6f;
    private float teachersRoomMaxX = 11.8f;
    
    private float homeHeight = 40f;
    public float homeThreshold = 30f;
    private float homeMinX = -21f;
    private float homeMaxX = 21f;

    private float spaceportHeight = 2.2f;
    private float spaceportMinX = -70f;
    private float spaceportMaxX = 45f;

    public SideScrollingCamera Instance { get; private set; }

    private void LateUpdate()
    {
        ClampCameraInScene();
    }

    public void MoveCamera(Vector2 newPosition)
    {
        Vector3 cameraPosition = transform.position;
        
        switch (scene)
        {
            case "Spaceport":
            {
                cameraPosition.y = spaceportHeight;
                break;
            }
            case "AllCombined":
            {
                if (newPosition.y > teachersRoomThreshold && newPosition.y < homeThreshold)
                {
                    cameraPosition.y = teachersRoomHeight + hallwayHeight;
                }
                else if (newPosition.y > homeThreshold)
                {
                    cameraPosition.y = homeHeight + hallwayHeight;
                }
                else if (newPosition.y < classroomThreshold)
                {
                    cameraPosition.y = classroomHeight + hallwayHeight;
                }
                else
                {
                    cameraPosition.y = hallwayHeight;
                }
                break;
            }
            default:
            {
                Debug.LogError("Scene Name incorrect! Check it again in Main Camera object");
                break;
            }
        }
        
        cameraPosition.x = newPosition.x;
        transform.position = cameraPosition;
    }

    private void ClampCameraInScene()
    {
        float minX, maxX;
        Vector3 cameraPosition = transform.position;

        switch (scene)
        {
            case "Spaceport":
            {
                minX = spaceportMinX;
                maxX = spaceportMaxX;
                break;
            }
            case "AllCombined":
            {
                if (transform.position.y > teachersRoomThreshold && transform.position.y < homeThreshold)
                {
                    minX = teachersRoomMinX;
                    maxX = teachersRoomMaxX;
                }
                else if (transform.position.y > homeThreshold)
                {
                    minX = homeMinX;
                    maxX = homeMaxX;
                }
                else if (transform.position.y < classroomThreshold)
                {
                    minX = classroomMinX;
                    maxX = classroomMaxX;
                }
                else
                {
                    minX = hallwayMinX;
                    maxX = hallwayMaxX;
                }
                break;
            }
            default:
            {
                minX = 0f;
                maxX = 0f;
                Debug.LogError("Scene Name incorrect! Check it again in Main Camera object");
                break;
            }
        }
        
        cameraPosition.x = Mathf.Clamp(trackedObject.position.x, minX + 0.5f, maxX - 0.5f);
        transform.position = cameraPosition;
    }

    public void SetClassroom(bool classroom)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = classroom ? classroomHeight : hallwayHeight;
        transform.position = cameraPosition;
    }

    public void SetTeachersRoom(bool teachersRoom)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = teachersRoom ? teachersRoomHeight : hallwayHeight;
        transform.position = cameraPosition;
    }

}
