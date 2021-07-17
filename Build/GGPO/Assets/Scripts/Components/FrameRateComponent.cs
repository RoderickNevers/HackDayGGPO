using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateComponent : MonoBehaviour
{
    const int RATE_LOCK = 60;
    void Start()
    {
        Time.captureFramerate = RATE_LOCK;
        Application.targetFrameRate = RATE_LOCK;
    }
}