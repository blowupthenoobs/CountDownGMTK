using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private Vector2 normalizedInput;

    private Rigidbody2D rb;
    
    public float speed;
    
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();   
    }

    void FixedUpdate()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        normalizedInput = new Vector2(hInput, vInput).normalized;
        
        rb.MovePosition(rb.position + normalizedInput * speed * Time.fixedDeltaTime);
    }
}
