using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float damageAmount = 1.0f;
    [SerializeField] private float attackTime = 1.0f;
    private RaycastHit2D[] hits;

    private float attackTimeCounter = 0.0f;
    public bool shouldBeDamaging { get; private set; } = false;
    public bool HasTakenDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private List<IDamageable> iDamageables = new List<IDamageable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Attack") && attackTimeCounter >= attackTime)
            {
            attackTimeCounter = 0.0f;
            Attack();
            //anim.SetTrigger("Attack");
            }

        attackTimeCounter += Time.deltaTime;
    }


    
    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange,transform.right, 0f , enemyLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
            
            if (iDamageable != null)
            {
                Debug.Log("Hit");
                iDamageable.Damage(damageAmount);
            }
        }

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
    /*
    public IEnumerator DamageWhileSlashIsActive()
    {
        shouldBeDamaging = true;
        while (shouldBeDamaging)
        {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, enemyLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.GetComponent<IDamageable>();

            if (iDamageable != null && !iDamageable.HasTakenDamage)
            {
                iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
                }
        }

        yield return null;

        }

        ReturnAttackablesToDamageable();

    }
    */
    private void ReturnAttackablesToDamageable()
    {
        foreach (IDamageable thingThatWasDamaged in iDamageables)
        {
            thingThatWasDamaged.HasTakenDamage = false;
        }
        iDamageables.Clear();
    }
    private void FixedUpdate()
    {
        
    }


}
