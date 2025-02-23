using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGame : MonoBehaviour
{
    public GameObject menu;

    public void StartGame()
    {
        SceneManager.LoadScene("Track2"); // Load the game scene
    }

    public void OpenSettings()
    {
        Debug.Log("Open Settings Menu");
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }

    public void OpenMainMenu()
    {
        menu.SetActive(true); // Display the main menu
        Time.timeScale = 0; // Pause the game
    }

    public void CloseMainMenu()
    {
        menu.SetActive(false); // Hide the main menu
        Time.timeScale = 1; // Resume the game
    }
}