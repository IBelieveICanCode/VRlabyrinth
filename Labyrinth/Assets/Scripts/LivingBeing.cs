using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LivingBeing : MonoBehaviour, IDamageable
{
    private bool isDestroying = false;

    [SerializeField]
    protected Weapon _weapon;
    [SerializeField]
    protected int _maxHealth = 100;
    protected int _health;
    public int Health => _health;

    void Start()
    {
        _health = _maxHealth;
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void ReceiveDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0 && !isDestroying)
        {
            isDestroying = true;
            Die();
        }
    }
}
