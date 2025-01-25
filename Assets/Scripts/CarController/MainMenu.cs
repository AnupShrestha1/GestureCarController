using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameLevel1()
    {
        SceneManager.LoadScene("Track1"); // Replace with your game scene name
    }
    public void StartGameLevel2()
    {
        SceneManager.LoadScene("Track2"); // Replace with your game scene name
    }

    public void OpenSettings()
    {
        // Show settings menu (implement Settings Menu logic here)
        Debug.Log("Open Settings Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}