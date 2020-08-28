using System;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
public class GrenadeLauncher : Weapon
{
    [SerializeField]
    private float _angle;
    private int _pooledBullets = 1;
    protected Pool<Projectile> _projPool;

    void Start()
    {
        _projPool = new Pool<Projectile>(new PrefabFactory<Projectile>(_projectilePrefab.gameObject), _pooledBullets);
    }
    protected override void Shoot(Transform target)
    {
        Projectile bullet = _projPool.Allocate();
        EventHandler handler = null;
        handler = (sender, e) => {
            _projPool.Release(bullet);
            bullet.Death -= handler;
        };
        bullet.Death += handler;
        bullet.gameObject.SetActive(true);
        bullet.gameObject.transform.position = transform.position;
        bullet.Init(_damage, CalculateArc(target));
    }

    Vector3 CalculateArc(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);

        float x = directionXZ.magnitude;
        float y = direction.y;
        float g = Physics.gravity.magnitude;
        float rad = _angle * Mathf.Deg2Rad;
        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(rad) * x) * Mathf.Pow(Mathf.Cos(rad), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        return direction.normalized * v;
    }

    protected override void Reset()
    {
    }
}
