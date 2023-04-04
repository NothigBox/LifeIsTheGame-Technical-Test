using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ExplosiveProjectileData : IProjectileData
{
    [SerializeField] private float force;
    [SerializeField] private float radius;
    [SerializeField] private float lifeTime;

    public float Force => force;
    public float Radius => radius;
    public float LifeTime => lifeTime;
}
