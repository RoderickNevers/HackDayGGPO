using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GGPOComponent : MonoBehaviour
{
    [DllImport("GGPO")]
    public static extern int ggponet_start_session(int number);

    void Start()
    {
        
    }
}
