using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FrictionShotData : IShotData
{
    [SerializeField] private float drag;
    [SerializeField] private float force;

    public float Drag => drag;
    public float Force => force;
}
