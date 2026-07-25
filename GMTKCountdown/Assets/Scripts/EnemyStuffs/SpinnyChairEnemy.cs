using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpinnyChairEnemy : EnemyScript
{
    private bool attacking;
    [SerializeField] GameObject coffeeCup;
    [SerializeField] float minDistToAttack;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float timeAfterAttacks;
    [SerializeField] float minProjectileSpeed, maxProjectileSpeed;
    [SerializeField] float minOrbitRange, maxOrbitRange, orbitSpeed;
    [SerializeField] float movementModeDuration;
    [SerializeField] float minWallDist, maxDistFromPlayer;
    [SerializeField] float dashSpeed;
    private float currentModeDuration;
    private int currentMode = -1;
    private Vector2 lastDashedDirection;

    void FixedUpdate()
    {
        //should have 3 modes: orbiting player slowly, dashing, and dashing back

        if(CanSeePlayer())
        {
            if(currentMode == -1)
                StartCoroutine(Moving());

            if(!attacking)
            {
                if((target.transform.position - transform.position).magnitude <= minDistToAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private IEnumerator Attack()
    {
        attacking = true;
        var direction = lastSeenPlayerPosition - transform.position;

        yield return new WaitForSeconds(timeBetweenAttacks);
        rb.velocity = new Vector2();
        ThrowProjectile();
        yield return new WaitForSeconds(timeBetweenAttacks);
        ThrowProjectile();
        yield return new WaitForSeconds(timeBetweenAttacks);
        ThrowProjectile();
        yield return new WaitForSeconds(timeAfterAttacks);
        attacking = false;
    }

    private IEnumerator Moving()
    {
        rb.velocity = new Vector2();
        currentMode = Random.Range(0, 3); //0 - orbit, 1 - dash, 2 - dash backwards //Temp took out orbit cuz it was being kinda wonky

        if(currentMode == 0)
        {
            var distFromPlayer = (transform.position - target.transform.position).magnitude;
            if(distFromPlayer <= minOrbitRange || maxOrbitRange <= distFromPlayer)
                currentMode = -1;
            else
            {
                currentModeDuration = 0;
                while(currentModeDuration < movementModeDuration && !((transform.position - target.transform.position).magnitude <= minOrbitRange || maxOrbitRange <= (transform.position - target.transform.position).magnitude) && target != null)
                {
                    // Debug.Log("called orbit");
                    if(target != null)
                        OrbitPlayer((transform.position - target.transform.position).magnitude, orbitSpeed);
                    currentModeDuration += Time.fixedDeltaTime;
                    Debug.Log(currentModeDuration);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(0f);
        }
        else if(currentMode == 1)
        {
            Lunge(lastDashedDirection, -dashSpeed);
            lastDashedDirection *= -1;
            yield return new WaitForSeconds(movementModeDuration);
        }
        else
        {
            var randomDegree = Random.Range(0, 360);
            var newDirection  = new Vector2(Mathf.Cos(randomDegree * Mathf.Deg2Rad), Mathf.Sin(randomDegree * Mathf.Deg2Rad));

            RaycastHit2D hit = Physics2D.Raycast(newDirection, transform.position, minWallDist);
            var newPoint = (Vector2)transform.position + newDirection * minWallDist;

            if(hit && ((Vector2)target.transform.position - newPoint).magnitude > maxDistFromPlayer)
                yield return new WaitForSeconds(0f);
            else
            {
                lastDashedDirection = newDirection;
                Lunge(lastDashedDirection, dashSpeed);
                yield return new WaitForSeconds(movementModeDuration);
            }
        }

        currentMode = -1;
    }

    private void ThrowProjectile()
    {
        var thrownItem = CastProjectile(coffeeCup);
        thrownItem.GetComponent<LobbedEnemyProjectileScript>().Initialize(Quaternion.Euler(0, 0, Random.Range(0, 360)), Random.Range(minProjectileSpeed, maxProjectileSpeed));
    }
}
 