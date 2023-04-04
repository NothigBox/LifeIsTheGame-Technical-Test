using UnityEngine;

public class BlackHoleProjectile : Projectile<BlackHoleProjectileData>
{
    public override void SetData(BlackHoleProjectileData projectileData)
    {
        base.SetData(projectileData);

        _Rigidbody.mass = data.Mass;

        var newCol = new GameObject("BlackHoleCollider");
        newCol.transform.SetParent(transform);
        newCol.transform.localPosition = Vector3.zero;
        newCol.transform.localRotation = Quaternion.identity;
        newCol.transform.localScale = Vector3.one;

        var sphereCollider = newCol.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = data.Radius;
    }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, data.Radius);
    }
}
