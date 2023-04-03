using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotsInfo", menuName = "ScriptableObjects/ShotsInfo", order = 1)]
public class ShotInfo : ScriptableObject
{
    [SerializeField] private WeaponInfo[] weaponInfos;

    public void Shot(string id, GameObject projectile)
    {
        foreach (var weaponInfo in weaponInfos)
        {
            if(weaponInfo.ID == id) 
            {
                switch (weaponInfo.ProjectileMode)
                {
                    case ProjectileMode.None:
                        break;

                    case ProjectileMode.BlackHole:
                        var blackHole = projectile.AddComponent<BlackHoleProjectile>();
                        blackHole.SetData((BlackHoleProjectileData) weaponInfo.projectileData);
                        break;

                    case ProjectileMode.Explosion:
                        break;
                }

                switch (weaponInfo.ShotMode)
                {
                    case ShotMode.Parabolic:
                        Vector3 forward = projectile.transform.forward;

                        var parabolicShotData = (ParabolicShotData) weaponInfo.shotData;
                        var parabilicRigidBody = projectile.GetComponent<Rigidbody>();
                        if(parabilicRigidBody == null) parabilicRigidBody = projectile.AddComponent<Rigidbody>();

                        parabilicRigidBody.AddForce(Vector3.up * parabolicShotData.ForceY, ForceMode.Impulse);
                        parabilicRigidBody.AddForce(new Vector3(forward.x, 0f, forward.z) * parabolicShotData.ForceXZ, ForceMode.Impulse);
                        break;

                    case ShotMode.Direct:
                        var directShotData = (DirectShotData) weaponInfo.shotData;
                        var directRigidbody = projectile.GetComponent<Rigidbody>();
                        if(directRigidbody == null) directRigidbody = projectile.AddComponent<Rigidbody>();
                        directRigidbody.useGravity = false;

                        directRigidbody.AddForce(projectile.transform.forward * directShotData.Speed, ForceMode.VelocityChange);
                        break;

                    case ShotMode.Spiral:
                        break;
                }
            }
        }
    }

    public WeaponInfo GetWeaponInfo(string id)
    {
        foreach (var weaponInfo in weaponInfos) if (weaponInfo.ID == id) return weaponInfo;

        return default;
    }

    private void OnValidate()
    {
        for (int i = 0; i < weaponInfos.Length; i++)
        {
            var weaponInfo = weaponInfos[i];

            if (weaponInfo.HasChangedShotMode)
            {
                switch (weaponInfo.ShotMode)
                {
                    case ShotMode.Parabolic:
                        weaponInfo.shotData = new ParabolicShotData();
                        break;

                    case ShotMode.Direct:
                        weaponInfo.shotData = new DirectShotData();
                        break;

                    case ShotMode.Spiral:
                        weaponInfo.shotData = new ParabolicShotData();
                        break;
                }

                weaponInfo.lastShotMode = weaponInfo.ShotMode;
            }

            if (weaponInfo.HasChangedProjectileMode)
            {
                switch (weaponInfo.ProjectileMode)
                {
                    case ProjectileMode.None:
                        weaponInfo.projectileData = null;
                        break;

                    case ProjectileMode.BlackHole:
                        weaponInfo.projectileData = new BlackHoleProjectileData();
                        break;

                    case ProjectileMode.Explosion:
                        weaponInfo.projectileData = new BlackHoleProjectileData();
                        break;
                }

                weaponInfo.lastProjectileMode = weaponInfo.ProjectileMode;
            }
            
            weaponInfos[i] = weaponInfo;
        }
    }
}

public enum ShotMode { Parabolic, Direct, Spiral }
public enum ProjectileMode { None, BlackHole, Explosion }