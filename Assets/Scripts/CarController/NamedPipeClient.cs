using System.Collections;
using System.Collections.Generic;
using UnityEngine;using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using UnityEngine;

public class NamedPipeClient : MonoBehaviour
{
    private Thread pipeThread;
    private NamedPipeClientStream pipeClient;
    public GestureCarController carController; // Reference to the car controller script

    void Start()
    {
        // Start a separate thread for reading pipe messages from Python
        pipeThread = new Thread(ReadPipe);
        pipeThread.Start();
    }

    void Update()
    {
        // Here you can implement any Unity-specific logic
    }

    private void ReadPipe()
    {
        while (true)
        {
            try
            {
                using (pipeClient = new NamedPipeClientStream(".", "UnityPipe", PipeDirection.In))
                {
                    pipeClient.Connect();

                    using (StreamReader reader = new StreamReader(pipeClient))
                    {
                        while (pipeClient.IsConnected)
                        {
                            string message = reader.ReadLine();

                            if (int.TryParse(message, out int controlValue))
                            {
                                Debug.Log("Received control value: " + controlValue);
                                carController.currentControl = (GestureCarController.Control)controlValue;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error in pipe communication: " + e.Message);
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (pipeThread != null && pipeThread.IsAlive)
        {
            pipeThread.Abort();
        }
    }
}
