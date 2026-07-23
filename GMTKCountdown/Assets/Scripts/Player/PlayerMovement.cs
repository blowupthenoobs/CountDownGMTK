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

    private WeaponItem weaponItem;

    public float speed;

    [SerializeField] Transform weaponsSpawn;
    public GameObject currentWeapon;
    public int weaponIDCurrent;

    public GameObject weaponPlayerIsOver;
    public bool hasPickedUpItem;

    public GameObject[] WeaponItems;
    public GameObject weapon;

    [HideInInspector] public bool newItem;
    
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        weaponIDCurrent = 0;
        //0 = fists
        //1 = sword
        //2 = revolver
        //3 = shotgun
        //4 = sniper
    }

    void Update()
    {
        if(weaponPlayerIsOver != null)
        {
            weaponItem = weaponPlayerIsOver.GetComponent<WeaponItem>();
        }

        if(Input.GetKeyDown(KeyCode.E) && weaponPlayerIsOver != null)
        {
            if(weaponIDCurrent != 0)
            {
                Instantiate(WeaponItems[weaponIDCurrent - 1], transform.position, transform.rotation);
            }
            currentWeapon = weaponPlayerIsOver;
            weaponIDCurrent = currentWeapon.GetComponent<WeaponItem>().ID;
            weapon.GetComponent<PlayerWeapon>().ID = weaponIDCurrent;
            Destroy(weaponPlayerIsOver);
            hasPickedUpItem = true;
            weaponPlayerIsOver = null;
            newItem = true;
        }

        if((Input.GetKeyDown(KeyCode.Q)) && weaponIDCurrent != 0)
        {
            Instantiate(WeaponItems[weaponIDCurrent - 1], transform.position, transform.rotation);
            currentWeapon = null;
            weaponIDCurrent = 0;
            weapon.GetComponent<PlayerWeapon>().ID = weaponIDCurrent;
        }
    }

    void FixedUpdate()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        normalizedInput = new Vector2(hInput, vInput).normalized;
        
        rb.MovePosition(rb.position + normalizedInput * speed * Time.fixedDeltaTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            weaponPlayerIsOver = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Weapon")
        {
            weaponPlayerIsOver = null;
        }
    }
}
