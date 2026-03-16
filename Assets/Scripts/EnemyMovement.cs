using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]private float speed = 2f;
    public float detectionRadius = 5f;
    public Transform player;
    [SerializeField] public GameObject Target;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= detectionRadius)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = dir * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    void Update()
    {
        if (Target != null)
        {
            Vector3 Look = transform.InverseTransformPoint(Target.transform.position);
            float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg ;

            transform.Rotate(0, 0, Angle);
        }
    }
}
