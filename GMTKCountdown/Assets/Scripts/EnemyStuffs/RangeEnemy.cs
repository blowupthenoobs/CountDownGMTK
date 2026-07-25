using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileFireRate;
    [SerializeField] Animator animator;

    public Transform player;
    float timeBetweenSpawns;

    public bool canShoot;
    bool canRunAtPlayer;

    // Update is called once per frame
    void Update()
    {
        ProjectileFire();
        FacePlayer();

        rb.velocity = Vector3.zero;

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
            animator.SetTrigger("isAttacking");
            CastProjectile(projectile);
            timeBetweenSpawns = 0;
        }
        if(!canShoot)
        {
            timeBetweenSpawns = 0;
        }
    }
}
