public interface IDamageable
{
    float Health { get; }
    void ReceiveDamage(float Damage);
    void Die();
}