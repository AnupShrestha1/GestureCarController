using System;
using System.Collections;
using System.Collections.Generic;
using PG;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestureCarController : MonoBehaviour,ICarControl
{
    public Control currentControl=Control.None;

    private void Awake()
    {
        GetComponent<CarController>().CarControl = this;
    }
    
    private void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(0);
        }
    
    }

    public float Acceleration
    {
        get
        {
            switch (currentControl)
            {
                case Control.Acceleration:
                case Control.AccelerationLeft:
                case Control.AccelerationRight:
                    return 1;
                default:
                    return 0;
            }
        }
    }

    public float BrakeReverse
    {
        get
        {
            switch (currentControl)
            {
                case Control.Brake:
                case Control.BrakeRight:
                case Control.BrakeLeft:
                    return 1;
                default:
                    return 0;
            }
        }
    }

    public float Horizontal
    {
        get
        {
            switch (currentControl)
            {
                case Control.BrakeRight:
                case Control.AccelerationRight:
                case Control.Right:
                    return 1;
                case Control.BrakeLeft:
                case Control.AccelerationLeft:
                case Control.Left:
                    return -1;
                default:
                    return 0;
            }
        }
    }

    public float Pitch { get; }

    public bool HandBrake
    {
        get
        {
            return currentControl==Control.HandBrake;
        }
    }

    public bool Boost { get; }

    public enum Control
    {
        None=0,
        Acceleration=1,
        AccelerationRight=2,
        AccelerationLeft=3,
        Brake=4,
        BrakeRight=5,
        BrakeLeft=6,
        Right=7,
        Left=8,
        HandBrake=9
    }
}
