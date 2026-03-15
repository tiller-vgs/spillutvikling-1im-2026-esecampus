using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 2;
    public float currentHealth;
    private SpriteRenderer spriteRenderer;

    
    public bool HasTakenDamage { get; set; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        spriteRenderer.color = Color.red;

        

        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    private void Die()
    {
        DestroyingPlatform.points += 1f;
        Destroy(gameObject);

    }

    public void TakeDamage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    
}