using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrollingCamera : MonoBehaviour
{
    public Transform trackedObject;
    
    public float hallwayHeight;
    public float hallwayMinX;
    public float hallwayMaxX;

    public float classroomHeight;
    public float classroomThreshold;
    public float classroomMinX;
    public float classroomMaxX;

    public float teachersRoomHeight;
    public float teachersRoomThreshold;
    public float teachersRoomMinX;
    public float teachersRoomMaxX;
    
    public float homeHeight;
    public float homeThreshold;
    public float homeMinX;
    public float homeMaxX;


    private void LateUpdate()
    {
        // cameraPosition.x = trackedObject.position.x;
        float minX, maxX;
        Vector3 cameraPosition = transform.position;

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
        
        cameraPosition.x = Mathf.Clamp(trackedObject.position.x, minX + 0.5f, maxX - 0.5f);
        transform.position = cameraPosition;
    }

    public void MoveCharacter(float newPosition)
    {
        Vector3 cameraPosition = transform.position;
        
        if (newPosition > teachersRoomThreshold && newPosition < homeThreshold)
        {
            cameraPosition.y = teachersRoomHeight + hallwayHeight;
        }
        else if (newPosition > homeThreshold)
        {
            cameraPosition.y = homeHeight + hallwayHeight;
        }
        else if (newPosition < classroomThreshold)
        {
            cameraPosition.y = classroomHeight + hallwayHeight;
        }
        else
        {
            cameraPosition.y = hallwayHeight;
        }
        
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