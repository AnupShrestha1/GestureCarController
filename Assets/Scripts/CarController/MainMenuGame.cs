using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGame : MonoBehaviour
{
    public GameObject menu;

    public void StartGame()
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

    public void GotoMainMenu(){
        SceneManager.LoadScene("MainMenu"); 
    }

    public void OpenMainMenu()
    {
         // Replace with your main menu scene name
         menu.SetActive(true);
         Time.timeScale = 0;
    }

    public void CloseMainMenu()
    {
         // Replace with your main menu scene name
         menu.SetActive(false);
         Time.timeScale = 1;
    }
}