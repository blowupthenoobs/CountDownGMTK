using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int health;

    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Enemy"))
       {
           health--;
       }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
