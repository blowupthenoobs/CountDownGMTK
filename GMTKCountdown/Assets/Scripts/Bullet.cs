using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    [SerializeField] float speed;
    [SerializeField] int damage;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(speed, 0, 0 * Time.fixedDeltaTime);
    }
}
