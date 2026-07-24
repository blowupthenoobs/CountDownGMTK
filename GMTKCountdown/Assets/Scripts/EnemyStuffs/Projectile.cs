using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;

    RangeEnemy rangeEnemy;
    PlayerHealth playerHealth;

    [SerializeField] int damage;
    Vector2 direction;

    // Start is called before the first frame update
    void Awake()
    {
        rangeEnemy = FindFirstObjectByType<RangeEnemy>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        rangeEnemy.player = GameObject.FindGameObjectWithTag("Player").transform;

        Direction();
    }

    void Update()
    {
        Movement();
    }

    void Direction()
    {
        direction = (rangeEnemy.player.position - transform.position).normalized;
    }
    void Movement()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerHealth.health -= damage;
            Destroy(gameObject);
        }
    }
}