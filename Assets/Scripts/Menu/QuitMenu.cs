using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    [SerializeField]
    GameObject parentMenu;

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        gameObject.SetActive(false);

        parentMenu.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        if (parentMenu != null)
        {
        }
        else
        {
            Debug.LogWarning("Main Menu is not assigned in the Inspector!");
        }

    }
}
