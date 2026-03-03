using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 2;
    public float currentHealth;

    public bool HasTakenDamage { get; set; }

    void Awake() => currentHealth = maxHealth;

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;


        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    private void Die()
    {
        Destroy(gameObject);

    }

    public void TakeDamage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    
}