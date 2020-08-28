using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using System;

public class MachineGunWeapon : Weapon
{
    private bool _isReloading = false;
    [SerializeField]
    private float _fireRate = 0.2f;
    
    [SerializeField]
    private int _maxAmmoCapacity = 10;
    private int _ammo;

    private int _pooledBullets = 10;
    protected Pool<Projectile> _projPool;
    Timer _timer;

    void Start()
    {
        _projPool = new Pool<Projectile>(new PrefabFactory<Projectile>(_projectilePrefab.gameObject), _pooledBullets);
        _ammo = _maxAmmoCapacity;
    }

    public override void Use(Transform target)
    {
        if (_ammo > 0)
        {
            if (Time.time > _nextSpawn)
            {
                Shoot(target);
                _nextSpawn = Time.time + _fireRate;
            }
        }
        else if (_ammo <= 0)
        {
            if (!_isReloading)
            {
                _isReloading = true;

                _timer = new Timer(2, Reload, true);
                _timer.Restart();
            }
        }

    }
    protected override void Reset()
    {
        Reload();
    }

    void Reload()
    {
        _isReloading = false;
        _ammo = _maxAmmoCapacity;
    }

    protected override void Shoot(Transform target)
    {
        Projectile bullet = _projPool.Allocate();
        EventHandler handler = null;
        handler = (sender, e) =>
        {
            _projPool.Release(bullet);
            bullet.Death -= handler;
        };
        bullet.Death += handler;
        bullet.gameObject.SetActive(true);
        bullet.Init(_damage, transform.forward);
        bullet.gameObject.transform.position = transform.position;
        AudioManager.PlayMusic("Bullet");
        _ammo--;
    }
}
