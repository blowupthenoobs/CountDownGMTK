using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;

    RangeEnemy rangeEnemy;
    PlayerHealth playerHealth;

    [SerializeField] int damage;
    Transform player;

    Vector2 direction;
    Vector2 moneyDirection;

    [SerializeField] bool isMoney;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Direction();
    }

    void Update()
    {
        Movement();
    }

    void Direction()
    {
        direction = (player.position - transform.position).normalized;

        if(isMoney)
        {
            direction = (player.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0,angle);
        }
    }
    void Movement()
    {
        transform.position += (Vector3) direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerHealth.health -= damage;
            Destroy(gameObject);
        }

        if(other.CompareTag("Crate"))
        {
            Destroy(gameObject);
        }

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}