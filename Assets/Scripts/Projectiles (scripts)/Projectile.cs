using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile<T> : MonoBehaviour where T : IProjectileData
{
    protected T data;

    new private Rigidbody rigidbody;

    public Rigidbody _Rigidbody => rigidbody?? GetComponent<Rigidbody>();

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void SetData(T projectileData) 
    {
        data = projectileData;
        _Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
}
