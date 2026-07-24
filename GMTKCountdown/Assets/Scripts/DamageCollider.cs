using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] int swordDamage;
    EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Collided");
            enemyScript = other.GetComponentInParent<EnemyScript>();

            if(enemyScript != null )
            {
                enemyScript.RecieveDamage(swordDamage);
                // Debug.Log("Enemy Health:" + enemyScript.health);
            }
            else
            {
                Debug.Log("No Enemy Script");
            }
        }
    }
}
