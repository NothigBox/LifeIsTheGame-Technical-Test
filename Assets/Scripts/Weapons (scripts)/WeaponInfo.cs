using System;
using UnityEngine;

[Serializable]
public struct WeaponInfo
{
    [SerializeField] private string id;
    
    [Header("Shot")]
    [SerializeField] private ShotMode shotMode;
    [SerializeReference] public IShotData shotData;
    [HideInInspector] public ShotMode lastShotMode;
    [Space]

    [Header("Projectile")]
    [SerializeField] private GameObject projectileModel;
    [SerializeField] private ProjectileMode projectileMode;
    [SerializeReference] public IProjectileData projectileData;
    [HideInInspector] public ProjectileMode lastProjectileMode;

    public string ID => id;
    public ShotMode ShotMode => shotMode;
    public ProjectileMode ProjectileMode => projectileMode;
    public GameObject ProjectileModel => projectileModel;
    public bool HasChangedShotMode => lastShotMode != shotMode;
    public bool HasChangedProjectileMode => lastProjectileMode != projectileMode;
}
