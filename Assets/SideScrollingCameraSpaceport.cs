using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollingCameraSpaceport : MonoBehaviour
{
    public Transform trackedObject;
    
    public float spaceportHeight;
    public float spaceportMinX;
    public float spaceportMaxX;


    private void LateUpdate()
    {
        // cameraPosition.x = trackedObject.position.x;
        float minX, maxX;
        Vector3 cameraPosition = transform.position;

        
        minX = spaceportMinX;
        maxX = spaceportMaxX;
        
        
        cameraPosition.x = Mathf.Clamp(trackedObject.position.x, minX + 0.5f, maxX - 0.5f);
        transform.position = cameraPosition;
    }

    public void MoveCharacter(float newPosition)
    {
        Vector3 cameraPosition = transform.position;
        
        cameraPosition.y = spaceportHeight;
        
        
        transform.position = cameraPosition;
    }


}