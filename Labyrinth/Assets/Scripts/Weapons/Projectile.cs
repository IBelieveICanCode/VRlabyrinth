using System;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IResettable
{
    public event EventHandler Death;
    protected float _damage;
    [SerializeField]
    protected float _speed = 10;
    [SerializeField]
    protected Rigidbody _rb;

    public virtual void Init(float damage, Vector3 direction)
    {
        _damage = damage;
        _rb.velocity = (direction * _speed);
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
