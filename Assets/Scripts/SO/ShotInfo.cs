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
                        var parabolicShotData = (ParabolicShotData) weaponInfo.shotData;
                        var parabilicRigidBody = projectile.AddComponent<Rigidbody>();

                        //TO DO: Make that parabolic shot work with the projectileSpawnPoint rotation.
                        /*
                        Vector3 force = parabolicShotData.ForceVector.normalized;
                        Vector3 forward = projectile.transform.forward;
                        Vector3 shotVector = new Vector3(forward.x * force.x, forward.y * force.y, forward.z * force.z);

                        parabilicRigidBody.AddForce(shotVector * parabolicShotData.InitialVelocity, ForceMode.Impulse);
                        */

                        parabilicRigidBody.AddForce(parabolicShotData.ForceVector, ForceMode.Impulse);
                        break;

                    case ShotMode.Direct:
                        var directShotData = (DirectShotData) weaponInfo.shotData;
                        var directRigidbody = projectile.AddComponent<Rigidbody>();
                        directRigidbody.useGravity = false;

                        directRigidbody.AddForce(projectile.transform.forward * directShotData.Speed, ForceMode.VelocityChange);
                        break;

                    case ShotMode.Free:
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

                    case ShotMode.Free:
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

public enum ShotMode { Parabolic, Direct, Free }
public enum ProjectileMode { None, BlackHole, Explosion }