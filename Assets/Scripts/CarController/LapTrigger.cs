using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    private LapTimer lapTimer;

    void Start()
    {
        // Get the LapTimer component
        lapTimer = FindObjectOfType<LapTimer>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Make sure the lap timer starts when the player passes the trigger
        if (other.CompareTag("Player"))
        {
            lapTimer.StartTimer();
        }
    }
}