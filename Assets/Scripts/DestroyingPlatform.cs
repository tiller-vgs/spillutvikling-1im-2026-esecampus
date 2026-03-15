using UnityEngine;

public class DestroyingPlatform : MonoBehaviour
{
    public static float points = 0f;
    [SerializeField] public float pointRequirement = 10f;
    [SerializeField] private ParticleSystem myParticleSystem;
    private bool onlyOnce = true;
    private bool isDestroyed = false;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed)
        {
            timer += Time.deltaTime;

            if (timer + 2f >= myParticleSystem.main.duration)
            {
                Destroy(gameObject);
            }
        }
        
        if (points >= pointRequirement && onlyOnce)
        {
            onlyOnce = false;   
            myParticleSystem.Play();
            isDestroyed = true;

            // unity particle system for breaking
            // play sound of it breaking cuz it cool

        }

    }
}
