using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrollingCamera : MonoBehaviour
{
    public Transform trackedObject;
    
    public float height = 6.5f;
    public float classroomHeight = -11.5f;
    public float classroomThreshold = 0f;

    public float teachersRoomHeight = 24f;
    public float teachersRoomThreshold = 10f;
    
    public float homeHeight = 45f;
    public float homeThreshold = 30f;
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = trackedObject.position.x;
        transform.position = cameraPosition;
        
    }
    public void MoveCharacter(float newPosition)
{
    Vector3 cameraPosition = transform.position;
    
    
    if (newPosition > teachersRoomThreshold && newPosition < homeThreshold)
    {
        cameraPosition.y = teachersRoomHeight;
    }
    else if (newPosition > homeThreshold)
    {
        cameraPosition.y = homeHeight;
    }
    else if (newPosition < classroomThreshold)
    {
        cameraPosition.y = classroomHeight;
    }
    else
    {
        cameraPosition.y = height;
    }
    
    transform.position = cameraPosition;
}

    public void SetClassroom(bool classroom)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = classroom ? classroomHeight : height;
        transform.position = cameraPosition;
        
    }
    public void SetTeachersRoom(bool teachersRoom)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = teachersRoom ? teachersRoomHeight : height;
        transform.position = cameraPosition;
        
    }


}