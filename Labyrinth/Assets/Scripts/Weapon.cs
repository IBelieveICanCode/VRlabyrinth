using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float _reload = 2f;
    protected float _nextSpawn = 0;

    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected Projectile _projectilePrefab;

    public virtual void Use()
    {
        if (Time.time > _nextSpawn)
        {
            SpawnProjectile();
            _nextSpawn = Time.time + _reload;
        }
    }

    protected abstract void SpawnProjectile();

}
