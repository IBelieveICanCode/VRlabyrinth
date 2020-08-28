using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float _reload = 2f;
    protected float _nextSpawn = 0;

    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected Projectile _projectilePrefab;

    public virtual void Use(Transform target)
    {
        if (Time.time > _nextSpawn)
        {
            Shoot(target);
            _nextSpawn = Time.time + _reload;
        }
    }

    protected abstract void Shoot(Transform target);

    protected abstract void Reset();

}
