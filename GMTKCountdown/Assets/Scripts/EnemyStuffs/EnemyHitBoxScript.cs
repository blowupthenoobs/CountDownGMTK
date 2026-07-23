using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxScript : MonoBehaviour
{
    public void RecieveDamage(int damage)
    {
        transform.parent.gameObject.SendMessage("RecieveDamage", damage);
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log("hit");
    // }
}
