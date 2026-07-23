using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Slider healthBar;

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        Debug.Log(health);
    }
}
