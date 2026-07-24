using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    [SerializeField] Slider healthBar;

    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        Debug.Log(health);
    }
}
