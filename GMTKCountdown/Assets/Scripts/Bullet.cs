using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    [SerializeField] float speed;
    [SerializeField] int damage;  
    public float slowdownSpeed;
    public int ID;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Movement();
        if(speed <= 0.01)
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
            other.gameObject.SendMessage("RecieveDamage", damage, SendMessageOptions.DontRequireReceiver);

        if(other.GetComponent<Rigidbody2D>() && !other.CompareTag("Bullet") && !other.CompareTag("EnemyDontCollide"))
        {
            Debug.Log(other);
            Destroy(gameObject);
        }
    }
}
