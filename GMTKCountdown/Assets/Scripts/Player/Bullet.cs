using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    [SerializeField] float speed;
    [SerializeField] int damage;  
    public float slowdownSpeed;
    public int ID;

    public GameObject[] weaponItems;
    public float dropChance;

    [SerializeField] GameObject shootSound;

    void Awake()
    {
        Instantiate(shootSound);
    }

    void FixedUpdate()
    {
        Movement();
        if(speed <= 0.1)
        {
            Destroy(gameObject, 0.3f);
        }
    }

    void Movement()
    {
        transform.Translate(speed, 0, 0 * Time.fixedDeltaTime);
        speed *= slowdownSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyCollide"))
        {
            other.gameObject.SendMessage("RecieveDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

        if(other.GetComponent<Rigidbody2D>() && !other.CompareTag("Bullet") && !other.CompareTag("EnemyDontCollide") && !other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if(other.CompareTag("Crate"))
        {
            if(Random.Range(1, 100) <= dropChance)
            {
                int randomNum = Random.Range(0, 4);
                Instantiate(weaponItems[randomNum], new Vector3(transform.position.x, transform.position.y - .35f, 0f), transform.rotation);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
