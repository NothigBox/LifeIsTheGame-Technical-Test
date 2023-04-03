using System;
using UnityEngine;

[Serializable]
public struct BlackHoleProjectileData : IProjectileData
{
    [SerializeField] private float force;
    [SerializeField] private float radius;

    public float Force => force;
    public float Radius => radius;
}
