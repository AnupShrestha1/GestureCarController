using UnityEngine;
using TMPro;

public class LapTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI lapTimeText;
    public TextMeshProUGUI bestLapTimeText;
    public TextMeshProUGUI bestLapTimeMenuText;
    public TextMeshProUGUI lapCountText;
    public GameObject gameOverPanel; // Reference to Game Over UI Panel
    public TextMeshProUGUI WinLose;
    public int thisLevel;
    public const string maxLevelKey="CompletedLevel";

    private LapTimer lapTimer;

    void Start()
    {
        lapTimer = FindObjectOfType<LapTimer>();

        if (lapTimer == null)
        {
            Debug.LogError("LapTimer not found in the scene.");
        }

        if (lapTimeText == null || bestLapTimeText == null || lapCountText == null || gameOverPanel == null)
        {
            Debug.LogError("One or more UI references are missing.");
        }

        gameOverPanel.SetActive(false); // Ensure it's hidden at the start
    }

    void Update()
    {
        if (lapTimer != null && Time.timeScale != 0) // Avoid updating when game is paused
        {
            lapTimeText.text = "Lap Time: " + lapTimer.LapTime.ToString("F2");
            bestLapTimeText.text = "Best Lap: " + lapTimer.GetBestLapTime().ToString("F2");
            lapCountText.text = "Lap: " + Mathf.Max(0, lapTimer.lapCount) + " / " + lapTimer.maxLaps;
            
        }
    }

    public void ShowGameOver()
    {
        Debug.Log("Showing Game Over UI");
        gameOverPanel.SetActive(true); // Display Game Over UI

        // Update best lap time only after the race ends
        bestLapTimeText.text = "Best Lap: " + lapTimer.GetBestLapTime().ToString("F2");
        bestLapTimeMenuText.text="Best Lap: " + lapTimer.GetBestLapTime().ToString("F2");
        if (lapTimer.GetBestLapTime() < 50)
        {
            WinLose.text = " You Won";
        }
        else
        {
            WinLose.text = "You Lose";
        }

        int maxLevel = PlayerPrefs.GetInt(maxLevelKey);
        if (maxLevel < thisLevel)
        {
            PlayerPrefs.SetInt(maxLevelKey,thisLevel);
        }
    }
}