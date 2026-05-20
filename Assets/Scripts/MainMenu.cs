using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public string startScene;

    void Start()
    {
         optionsPanel.SetActive(false);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }
}