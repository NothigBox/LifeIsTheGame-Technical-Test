using System;
using UnityEngine;

[Serializable]
public struct BlackHoleProjectileData : IProjectileData
{
    [SerializeField] private float atractionForce;
    [SerializeField] private float atractionRadius;
}
