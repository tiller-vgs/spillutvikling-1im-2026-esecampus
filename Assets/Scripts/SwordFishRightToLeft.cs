using UnityEngine;

public class SwordFishRightToLeft : MonoBehaviour
{
    [SerializeField]public float speed = 20f;          // I AM SPEED
    [SerializeField]public float destroyX = 20f;       // hvor den spawner og hvor labg den skal gå 

    void Update()
    {

        transform.Translate(Vector2.left * speed * Time.deltaTime);


        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
