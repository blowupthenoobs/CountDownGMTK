using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Manager : MonoBehaviour
{
    [HideInInspector] public bool enemiesDead;
    int amountOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        amountOfEnemies = GameObject.FindGameObjectsWithTag("EnemyDontCollide").Length;

        if(amountOfEnemies == 0)
        {
            enemiesDead = true;
        }
    }
}
