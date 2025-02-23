using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnabler : MonoBehaviour
{

    public Button Level1, Level2;
    // Start is called before the first frame update
    void Start()
    {
        int maxCompletedLevel = PlayerPrefs.GetInt(LapTimeDisplay.maxLevelKey, 0);
        if (maxCompletedLevel >= 1)
        {
            Level2.interactable = true;
        }
        else
        {
            Level2.interactable = false;
        }
    }
}
