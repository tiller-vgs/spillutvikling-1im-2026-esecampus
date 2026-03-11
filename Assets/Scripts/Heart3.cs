using UnityEngine;

public class Heart3 : MonoBehaviour
{
    private float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float health = GameObject.Find("Player").GetComponent<Player_health>().PlayerHealth;
        if (health < 1)
        {
            gameObject.SetActive(false);
        } 

        else
        {
            gameObject.SetActive(true);
        }
    }
}
