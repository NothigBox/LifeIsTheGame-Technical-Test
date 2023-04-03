using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DirectShotData : IShotData
{
    [SerializeField] private float speed;
    
    public float Speed => speed;
}
