using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ParabolicShotData : IShotData
{
    //[SerializeField] private float initialVelocity;
    [SerializeField] private Vector3 forceVector;

    //public float InitialVelocity => initialVelocity;
    public Vector3 ForceVector => forceVector;
}
