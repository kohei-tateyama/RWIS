using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{

    [SerializeField] GameObject currentTask;

    // Start is called before the first frame update
    void Start()
    {
        if (currentTask == null)
        {
            Debug.LogWarning("Dialogue Panel: currentTask is not assigned in the inspector");
        }
    }

 void OnEnable()
    {
        currentTask.SetActive(false);
    }

    void OnDisable()
    {
        currentTask.SetActive(true);
    }
}
