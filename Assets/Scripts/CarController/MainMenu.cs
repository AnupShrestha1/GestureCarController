using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace with your game scene name
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