using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meter Resource", menuName = "SystemDirection/MeterResource", order = 2)]
public class MeterResource : ScriptableObject
{
    [SerializeField] public bool Enabled = true;

    [SerializeField] public int SingleMeterCapacity = 100;
    [SerializeField] public int QuantityOfMetersToStore = 1;
}