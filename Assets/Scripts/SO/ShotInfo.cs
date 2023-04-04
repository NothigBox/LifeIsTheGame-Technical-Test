using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotsInfo", menuName = "ScriptableObjects/ShotsInfo", order = 1)]
public class ShotInfo : ScriptableObject
{
    [SerializeField] private WeaponInfo[] weaponInfos;

    public void Shot(string id, GameObject projectileObject)
    {
        foreach (var weaponInfo in weaponInfos)
        {
            if(weaponInfo.ID == id) 
            {
                switch (weaponInfo.ProjectileMode)
                {
                    case ProjectileMode.None:
                        projectileObject.AddComponent<DefaultProjectile>();
                        break;

                    case ProjectileMode.BlackHole:
                        var blackHole = projectileObject.AddComponent<BlackHoleProjectile>();
                        blackHole.SetData((BlackHoleProjectileData) weaponInfo.projectileData);
                        break;

                    case ProjectileMode.Explosive:
                        var explosive = projectileObject.AddComponent<ExplosiveProjectile>();
                        explosive.SetData((ExplosiveProjectileData) weaponInfo.projectileData);
                        break;
                }

                var projectileRb = projectileObject.GetComponent<Rigidbody>();

                switch (weaponInfo.ShotMode)
                {
                    case ShotMode.Parabolic:
                        Vector3 forward = projectileObject.transform.forward;

                        var parabolicShotData = (ParabolicShotData) weaponInfo.shotData;

                        projectileRb.AddForce(Vector3.up * parabolicShotData.ForceY, ForceMode.Impulse);
                        projectileRb.AddForce(new Vector3(forward.x, 0f, forward.z) * parabolicShotData.ForceXZ, ForceMode.Impulse);
                        break;

                    case ShotMode.Direct:
                        var directShotData = (DirectShotData) weaponInfo.shotData;

                        projectileRb.useGravity = false;

                        projectileRb.AddForce(projectileObject.transform.forward * directShotData.Speed, ForceMode.VelocityChange);
                        break;

                    case ShotMode.Friction:
                        var frictionShotData = (FrictionShotData) weaponInfo.shotData;

                        projectileRb.useGravity = false;
                        projectileRb.drag = frictionShotData.Drag;

                        projectileRb.AddForce(projectileObject.transform.forward * frictionShotData.Force, ForceMode.Impulse);
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

                    case ShotMode.Friction:
                        weaponInfo.shotData = new FrictionShotData();
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

                    case ProjectileMode.Explosive:
                        weaponInfo.projectileData = new ExplosiveProjectileData();
                        break;
                }

                weaponInfo.lastProjectileMode = weaponInfo.ProjectileMode;
            }
            
            weaponInfos[i] = weaponInfo;
        }
    }
}

public enum ShotMode { Parabolic, Direct, Friction }
public enum ProjectileMode { None, BlackHole, Explosive }