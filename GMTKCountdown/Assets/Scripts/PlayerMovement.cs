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

    [SerializeField] Transform weaponsSpawn;
    public GameObject currentWeapon;

    public GameObject weaponPlayerIsOver;
    public bool hasPickedUpItem;
    
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Weapon weapon = weaponPlayerIsOver.GetComponent<Weapon>();

        if ((Input.GetKeyDown(KeyCode.E)) && weapon.canPickup)
        {
            currentWeapon = weaponPlayerIsOver;
            Instantiate(weaponPlayerIsOver, weaponsSpawn.position, weaponsSpawn.rotation, weaponsSpawn.transform);
            hasPickedUpItem = true;
        }

        if((Input.GetKeyDown(KeyCode.Q)) && currentWeapon != null)
        {
            Instantiate(currentWeapon, transform.position, weaponsSpawn.transform.rotation);
        }
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
        if (other.tag == "Weapon")
        {
            weaponPlayerIsOver = other.gameObject;
        }
    }
}
