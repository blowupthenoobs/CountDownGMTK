using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExecutive : EnemyScript
{
    private bool seesPlayer;
    private bool attacking;
    
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
    }
}
