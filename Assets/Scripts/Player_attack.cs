using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackTransformRight;
    [SerializeField] private Transform attackTransformLeft;
    [SerializeField] private float damageAmount = 1.0f;
    [SerializeField] private float attackTime = 0.6f;
    private RaycastHit2D[] hits;

    private float attackTimeCounter = 0.0f;
    public bool shouldBeDamaging { get; private set; } = false;
    public bool HasTakenDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private List<IDamageable> iDamageables = new List<IDamageable>();

    [SerializeField] private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.IsPaused)
        {
            return;
        }
        else
        {

            if (Input.GetButtonDown("AttackRight") && attackTimeCounter >= attackTime)
            {
                attackTimeCounter = 0.0f;
                AttackRight();
            }
            if (Input.GetButtonDown("AttackLeft") && attackTimeCounter >= attackTime)
            {
                attackTimeCounter = 0.0f;
                AttackLeft();
            }

            attackTimeCounter += Time.deltaTime;
        }
    }

    private void AttackLeft()
    {
        anim.SetTrigger("playerAttack");
        hits = Physics2D.CircleCastAll(attackTransformLeft.position, attackRange, transform.right, 0f, enemyLayer);

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



    private void AttackRight()
    {
        anim.SetTrigger("playerAttack");
        hits = Physics2D.CircleCastAll(attackTransformRight.position, attackRange,transform.right, 0f , enemyLayer);

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
        Gizmos.DrawWireSphere(attackTransformRight.position, attackRange);
        Gizmos.DrawWireSphere(attackTransformLeft.position, attackRange);
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
