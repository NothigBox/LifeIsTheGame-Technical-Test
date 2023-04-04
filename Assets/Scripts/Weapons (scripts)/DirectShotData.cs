using System;
using UnityEngine;

[Serializable]
public struct DirectShotData : IShotData
{
    [SerializeField] private float speed;
    
    public float Speed => speed;
}
