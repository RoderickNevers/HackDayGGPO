using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateComponent : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 120;
        Debug.Log($"Refresh Rate: {Screen.currentResolution.refreshRate}");
    }
}