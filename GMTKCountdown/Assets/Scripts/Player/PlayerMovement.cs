using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private Vector2 normalizedInput;

    private Rigidbody2D rb;

    private WeaponItem weaponItem;

    public float speed;
    [SerializeField] WeaponControlScript weaponControlScript;

    public GameObject weaponPlayerIsOver;

    public GameObject[] WeaponItems;
    public GameObject weapon;

    [HideInInspector] public bool newItem;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(weaponPlayerIsOver != null)
        {
            weaponItem = weaponPlayerIsOver.GetComponent<WeaponItem>();
        }

        if(Input.GetKeyDown(KeyCode.E) && weaponPlayerIsOver != null)
        {
            // if(weaponIDCurrent != 0)
            // {
            //     Instantiate(WeaponItems[weaponIDCurrent - 1], transform.position, transform.rotation);
            // }
            // currentWeapon = weaponPlayerIsOver;
            // weaponIDCurrent = currentWeapon.GetComponent<WeaponItem>().ID;
            // weapon.GetComponent<PlayerWeapon>().ID = weaponIDCurrent;
            // Destroy(weaponPlayerIsOver);
            // hasPickedUpItem = true;
            // weaponPlayerIsOver = null;
            // newItem = true;

            weaponControlScript.PickUpWeapon(weaponPlayerIsOver);
        }

        if((Input.GetKeyDown(KeyCode.Q)))
        {
            // Instantiate(WeaponItems[weaponIDCurrent - 1], transform.position, transform.rotation);
            // currentWeapon = null;
            // weaponIDCurrent = 0;
            // weapon.GetComponent<PlayerWeapon>().ID = weaponIDCurrent;

            weaponControlScript.DropWeapon();
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
        if(other.gameObject == weaponPlayerIsOver)
        {
            weaponPlayerIsOver = null;
        }
    }
}
