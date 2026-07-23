using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExecutive : EnemyScript
{
    private bool seesPlayer;
    void Start()
    {
        
    }

    void Update()
    {
        seesPlayer = CanSeePlayer();
        // Debug.Log(CanSeePlayer());
    }

    void FixedUpdate()
    {
        if(seesPlayer)
            RunAtPlayer(defaultMoveSpeed);   
    }
}
