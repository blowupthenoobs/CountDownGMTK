using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExecutive : EnemyScript
{
    private bool attacking;
    [SerializeField] float attackWidth;
    [SerializeField] float minDistToAttack;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float timeAfterAttacks;
    [SerializeField] float lungePower;

    void Start()
    {
        
    }

    void Update()
    {
        FacePlayer();
    }

    void FixedUpdate()
    {
        if(!attacking)
        {
            rb.velocity = new Vector2();

            // Debug.Log("not attacking");
            if(CanSeePlayer())
            {
                RunAtPlayer(defaultMoveSpeed);
                animator.SetBool("isIdle", false);
            }

            if(target == null)
            {
                animator.SetBool("isIdle", true);
            }
            
            if(target != null)
            {
                if((target.transform.position - transform.position).magnitude <= minDistToAttack)
                {
                    attacking = true;
                    animator.SetTrigger("isAttacking");
                    Debug.Log("AttackTriggered");
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private IEnumerator Attack()
    {
        attacking = true;
        var direction = lastSeenPlayerPosition - transform.position;

        Lunge(direction, 5);
        yield return new WaitForSeconds(timeBetweenAttacks);
        rb.velocity = new Vector2();
        MeleeAttack(attackRange, 1f, Mathf.Atan2(direction.y, direction.x));
        yield return new WaitForSeconds(timeBetweenAttacks);
        MeleeAttack(attackRange, 1f, Mathf.Atan2(direction.y, direction.x));
        yield return new WaitForSeconds(timeBetweenAttacks);
        MeleeAttack(attackRange, 1f, Mathf.Atan2(direction.y, direction.x));
        yield return new WaitForSeconds(timeAfterAttacks);
        attacking = false;
    }
}