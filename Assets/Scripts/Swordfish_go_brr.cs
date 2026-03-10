using UnityEngine;

public class EnemyStraightMove : MonoBehaviour
{
    public float speed = 20f;          // I AM SPEED
    public float destroyX = 20f;       // hvor den spawner og hvor labg den skal gå 

    void Update()
    {
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        
        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }
}
