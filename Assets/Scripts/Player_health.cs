
using UnityEngine;

public class Player_health : MonoBehaviour, IDamageable
{
    public float PlayerHealth = 3;
    public bool PlayerDead = false;

    private BoxCollider2D playerCollider;

    public bool HasTakenDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = this.GetComponent<BoxCollider2D>();
        Debug.Log("Has collider player: ", playerCollider);
    }

    // Update is called once per frame
    void Update()
    {



        if (playerDead())
        {
            Die();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        PlayerHealth -= damageAmount;
    }

    private bool playerDead()
    {
        if (PlayerHealth < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Die()
    {
    Destroy(gameObject);
    }




    public void Damage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    
}
