using System;
using UnityEngine;

[Serializable]
public struct BlackHoleProjectileData : IProjectileData
{
    [SerializeField] private float orbitalSpeed;
    [SerializeField] private float effectRadius;

    public float OrbitalSpeed => orbitalSpeed;
    public float EffectRadius => effectRadius;
}
