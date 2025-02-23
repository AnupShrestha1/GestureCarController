using UnityEngine;
using UnityEngine.SceneManagement;

public class LapTimer : MonoBehaviour
{
    public float LapTime { get; private set; }
    private float bestLapTime = float.MaxValue;
    public int lapCount = 0;
    public int maxLaps = 3;

    private bool isRacing = false;
    private bool hasPassedFirstTime = false;

    void Update()
    {
        if (isRacing)
        {
            LapTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            EndGame();
        }
    }

    public void StartTimer()
    {
        isRacing = true;
        LapTime = 0f;
    }

    public void CompleteLap()
    {
        if (!hasPassedFirstTime)
        {
            hasPassedFirstTime = true;
            StartTimer();
            return;
        }

        if (LapTime < bestLapTime)
        {
            bestLapTime = LapTime;
        }

        lapCount++;

        if (lapCount >= maxLaps)
        {
            EndGame(); // Fix: Ensure UI updates before pausing the game
        }
        else
        {
            LapTime = 0f;
        }
    }

    public float GetBestLapTime()
    {
        return bestLapTime == float.MaxValue ? 0f : bestLapTime;
    }

    private void EndGame()
    {
        Debug.Log("Race Finished! Displaying Game Over Screen...");

        // Ensure the UI is updated before freezing the game
        LapTimeDisplay lapTimeDisplay = FindObjectOfType<LapTimeDisplay>();

        if (lapTimeDisplay != null)
        {
            lapTimeDisplay.ShowGameOver();
        }
        else
        {
            Debug.LogError("LapTimeDisplay script not found in the scene.");
        }

        // Delay pausing the game to allow UI to update
        Invoke("PauseGame", 0.5f);
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Now pause the game AFTER UI is shown
    }
}