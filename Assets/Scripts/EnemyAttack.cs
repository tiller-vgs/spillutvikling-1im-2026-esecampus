using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 1;
    public float attackRange = 1f;
    public float attackCooldown = 1f;

    Transform player;
    float cooldownTimer = 0f;

    void Awake()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= attackRange && cooldownTimer <= 0f)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        var damageable = player.GetComponent<IDamageable>(); // bruk interfarence (hopefully) for å få tilgang til TakeDamage
        if (damageable != null)
        {
            damageable.TakeDamage(attackDamage);
        }
        cooldownTimer = attackCooldown;
    }
}
