using UnityEngine;

public class LapTimer : MonoBehaviour
{
    public float lapTime = 0f;  // Time in seconds for the lap
    private bool isTiming = false;

    void Update()
    {
        // Make sure lapTime is being updated when the timer is running
        if (isTiming)
        {
            lapTime += Time.deltaTime;  // Increment lap time every frame
        }
    }

    public void StartTimer()
    {
        isTiming = true;
        lapTime = 0f;  // Reset lap time at the start of the lap
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public float GetLapTime()
    {
        return lapTime;  // Return the current lap time
    }
}