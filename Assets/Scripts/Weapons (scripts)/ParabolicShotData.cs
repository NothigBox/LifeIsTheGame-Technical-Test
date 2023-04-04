using System;
using UnityEngine;

[Serializable]
public struct ParabolicShotData : IShotData
{
    [SerializeField] private float forceY;
    [SerializeField] private float forceXZ;

    public float ForceY => forceY;
    public float ForceXZ => forceXZ;
}   
