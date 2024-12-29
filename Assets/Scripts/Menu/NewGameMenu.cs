using UnityEngine;
using UnityEngine.SceneManagement;


public class NewGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject parentMenu;

    public void YesButton()
    {
        SceneManager.LoadScene("1-1");
    }

    public void NoButton()
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
