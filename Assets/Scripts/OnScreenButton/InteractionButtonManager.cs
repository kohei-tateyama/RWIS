using UnityEngine;

public class InteractionButtonManager : MonoBehaviour
{
    private static InteractionButtonManager instance;

    private GameObject interactionButton;

    private void Awake()
    {
        instance = this;
        interactionButton = GameObject.Find("InteractButton");
        interactionButton.SetActive(false);
    }

    public static InteractionButtonManager GetInstance() 
    {
        return instance;
    }

    public void ShowButton()
    {
        interactionButton.SetActive(true);
    }

    public void HideButton()
    {
        interactionButton.SetActive(false);
    }

}
    