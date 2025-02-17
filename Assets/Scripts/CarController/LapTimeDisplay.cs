using UnityEngine;
using TMPro;

public class LapTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI lapTimeText;  // Reference to the TextMeshProUGUI component
    private LapTimer lapTimer;  // Reference to the LapTimer script

    void Start()
    {
        // Find the LapTimer component in the scene
        lapTimer = FindObjectOfType<LapTimer>();

        // Check if lapTimer was found
        if (lapTimer == null)
        {
            Debug.LogError("LapTimer not found in the scene.");
        }

        // Check if lapTimeText was assigned in the Inspector
        if (lapTimeText == null)
        {
            Debug.LogError("LapTimeText is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Update the lap time on the UI text if lapTimer exists
        if (lapTimer != null && lapTimeText != null)
        {
            // Display the lap time using the GetLapTime() method
            lapTimeText.text = "Lap Time: " + lapTimer.GetLapTime().ToString("F2");
        }
    }
}