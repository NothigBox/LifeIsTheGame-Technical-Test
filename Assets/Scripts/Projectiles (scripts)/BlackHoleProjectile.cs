using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BlackHoleProjectile : MonoBehaviour
{
    private BlackHoleProjectileData data;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            var otherRb = other.GetComponent<Rigidbody>();

            float mass = data.Mass;
            float otherMass = otherRb.mass;
            float distance = Vector3.Distance(transform.position, other.transform.position);

            float F = data.Force * ((mass * otherMass) / (distance * distance));

            otherRb.AddForce((transform.position - other.transform.position).normalized * F, ForceMode.Acceleration);
        }
    }

    public void SetData(BlackHoleProjectileData projectileData)
    {
        data = projectileData;

        GetComponent<Rigidbody>().mass = data.Mass;

        var newCol = new GameObject("BlackHoleCollider");
        newCol.transform.SetParent(transform);
        newCol.transform.localPosition = Vector3.zero;
        newCol.transform.localRotation = Quaternion.identity;

        var sphereCollider = newCol.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = data.Radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, data.Radius);
    }
}
