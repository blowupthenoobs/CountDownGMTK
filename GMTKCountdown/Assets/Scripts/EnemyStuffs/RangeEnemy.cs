using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileFireRate;

    public Transform player;
    float timeBetweenSpawns;

    public bool canShoot;
    bool canRunAtPlayer;

    [SerializeField] bool isMoneyEnemy;
    [SerializeField] bool isCoffeeEnemy;

    // Update is called once per frame
    void Update()
    {
        ProjectileFire();
        FacePlayer();
        rb.velocity = new Vector2();

        if(CanSeePlayer() && canRunAtPlayer)
        {
            RunAtPlayer(defaultMoveSpeed);
        }

        if(target != null)
        {
            if(target != null)
            {
                if (Vector2.Distance(transform.position, target.transform.position) < attackRange)
                {
                    canRunAtPlayer = false;
                    animator.SetBool("isIdle", true);
                    canShoot = true;
                }
                else
                {
                    canRunAtPlayer = true;
                    animator.SetBool("isIdle", false);
                    canShoot = false;
                }
            }
        }
    }

    void ProjectileFire()
    {
        timeBetweenSpawns += Time.deltaTime;

        if(projectileFireRate <= timeBetweenSpawns && canShoot)
        {
            if(isCoffeeEnemy)
            {
                animator.SetTrigger("isAttacking");
            }
            if(isMoneyEnemy)
            {
                animator.SetTrigger("MoneyAttack");
            }    

            CastProjectile(projectile);
            timeBetweenSpawns = 0;
        }
        if(!canShoot)
        {
            timeBetweenSpawns = 0;
        }
    }
}
