using System;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour, IResettable
{
    public event EventHandler Death;
    private int _damage;
    private Vector3 _direction;
    private float _speed = 1;
    [SerializeField]
    Rigidbody _rb;

    public void Init(int damage, Vector3 direction)
    {
        _damage = damage;
        _rb.AddForce(direction * _speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.ReceiveDamage(_damage);
            OnDeath();
        }
        else
        {
            OnDeath();
        }
    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }

    void OnDeath()
    {
        Death?.Invoke(this, null);
    }
}
