using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProjectile : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private float orbitalSpeed;

    private void Awake()
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            other.transform.RotateAround(transform.position, 10f);
        }
    }

    public void SetData(BlackHoleProjectileData projectileData)
    {
        sphereCollider.radius = projectileData.EffectRadius;
        orbitalSpeed = projectileData.OrbitalSpeed;
    }
}
