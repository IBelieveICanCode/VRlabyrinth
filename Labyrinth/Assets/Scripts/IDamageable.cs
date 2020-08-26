public interface IDamageable
{
    int Health { get; }
    void ReceiveDamage(int Damage);
    void Die();
}