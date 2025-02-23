using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public LapTimer lapTimer;  // Reference to LapTimer

    private void Start()
    {
        // Find LapTimer automatically if not assigned
        if (lapTimer == null)
        {
            lapTimer = FindObjectOfType<LapTimer>();
        }

        if (lapTimer == null)
        {
            Debug.LogError("LapTimer script not found! Make sure it is in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lapTimer.CompleteLap();  // Call CompleteLap() when the player crosses the trigger
        }
    }
}