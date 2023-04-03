using System;
using UnityEngine;

[Serializable]
public struct BlackHoleProjectileData : IProjectileData
{
    [SerializeField] private float force;
    [SerializeField] private float radius;
    [SerializeField] private float mass;

    public float Force => force;
    public float Radius => radius;
    public float Mass => mass;
}
