using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject MenuBar;
    public void StartButton()
    {
        SceneManager.LoadScene("Scenes");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
