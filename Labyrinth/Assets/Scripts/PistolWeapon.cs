using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using System;

public class PistolWeapon : Weapon
{
    private int _pooledBullets = 1;
    protected Pool<Projectile> _projPool;

    void Start()
    {
        _projPool = new Pool<Projectile>(new PrefabFactory<Projectile>(_projectilePrefab.gameObject), _pooledBullets);
    }
    protected override void SpawnProjectile()
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
        bullet.Init(_damage, bullet.transform.forward);
    }
}
