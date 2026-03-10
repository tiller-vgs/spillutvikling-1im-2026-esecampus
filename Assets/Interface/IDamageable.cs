using UnityEngine;

public interface IDamageable
{
    public void Damage(float damageAmount);
    
    public void TakeDamage(float damageAmount);

    public bool HasTakenDamage { get; set; }
}