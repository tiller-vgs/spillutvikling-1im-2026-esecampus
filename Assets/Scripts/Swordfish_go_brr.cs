using UnityEngine;

public class EnemyStraightMove : MonoBehaviour
{
    public float speed = 20f;          // I AM SPEED
    public float destroyX = 20f;    // hvor den spawner og hvor labg den skal gå 
    
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 1f;


    Transform player;
     float cooldownTimer = 0f;

    
    [SerializeField] private float attackCooldown = 0.5f;
    
    

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }
    void Update()
    {
       

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        cooldownTimer -= Time.deltaTime;
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= attackRange && cooldownTimer <= 0f)
        {
            if (PauseMenu.IsPaused)
            {
                Debug.Log("Enemy er slem");
            }
            else
            {
                TryAttack();
            }
        }





        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
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
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        if (collision.gameObject.CompareTag("Player") && cooldownTimer <= 0 )
        {
            TryAttack();
            Debug.Log("Colliding BUT ALSO attacking maybe");
        }


    }
}
