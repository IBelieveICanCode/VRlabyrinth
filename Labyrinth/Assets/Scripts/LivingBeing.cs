using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class LivingBeing : MonoBehaviour, IDamageable
{
    private bool isDestroying = false;
    [SerializeField]
    protected float _maxHealth = 100;
    [SerializeField]
    protected float _health;
    public float Health => _health;

    void Start()
    {
        _health = _maxHealth;
        Init();
    }
    protected abstract void Init();
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void ReceiveDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }
}
