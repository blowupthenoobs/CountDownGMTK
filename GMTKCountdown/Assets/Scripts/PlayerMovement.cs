using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private Vector2 normalizedInput;

    private Rigidbody2D rb;

    public float speed;

    [SerializeField] GameObject[] weapons;
    [SerializeField] Transform weaponsSpawn;
    
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();   
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        normalizedInput = new Vector2(hInput, vInput).normalized;
        
        rb.MovePosition(rb.position + normalizedInput * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Sword"))
        {
            Instantiate(weapons[0], weaponsSpawn.transform.position, weaponsSpawn.transform.rotation, weaponsSpawn.transform);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Revolver"))
        {
            Instantiate(weapons[1], weaponsSpawn.transform.position, weaponsSpawn.transform.rotation, weaponsSpawn.transform);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Shotgun"))
        {
            Instantiate(weapons[2], weaponsSpawn.transform.position, weaponsSpawn.transform.rotation, weaponsSpawn.transform);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Sniper"))
        {
            Instantiate(weapons[3], weaponsSpawn.transform.position, weaponsSpawn.transform.rotation, weaponsSpawn.transform);
            Destroy(other.gameObject);
        }
    }
}
