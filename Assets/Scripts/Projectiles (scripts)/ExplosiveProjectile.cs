using UnityEngine;

public class ExplosiveProjectile : Projectile<ExplosiveProjectileData>
{
    public override void SetData(ExplosiveProjectileData projectileData)
    {
        base.SetData(projectileData);

        Invoke(nameof(Explode), data.LifeTime);
    }

    private void Explode() 
    {
        var enemies = Physics.OverlapSphere(transform.position, data.Radius);

        foreach(var enemy in enemies) 
        {
            if (enemy.CompareTag("Enemy")) 
            {
                enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position).normalized * data.Force, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.Radius);
    }
}
