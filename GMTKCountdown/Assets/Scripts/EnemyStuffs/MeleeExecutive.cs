using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExecutive : EnemyScript
{
    private bool seesPlayer;
    private bool attacking;
    [SerializeField] Vector2 attackSize;

    void Start()
    {
        
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if(CanSeePlayer())
            RunAtPlayer(defaultMoveSpeed);   
        
        MeleeAttack(attackSize, lastSeenPlayerPosition - transform.position, 5);
    }
}
