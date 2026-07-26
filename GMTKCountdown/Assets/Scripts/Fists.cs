using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : GunScript
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector2 raycastSize;
    [SerializeField] int hitDamage;

    [SerializeField] float hitTime;
    float timeBetweenSwings;

    bool canHit;
    public bool noWeapon;

    [SerializeField] WeaponControlScript weaponControl;
    public bool hasHit;

    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Weapon is Null?" + (weaponControl.heldWeapon == null));

        if(weaponControl.heldWeapon != null)
        {
            Debug.Log("Has weapon - returning");
            noWeapon = false;
            return;
        }
        else if (weaponControl.heldWeapon == null)
        {
            noWeapon = true;

            Cooldown();
            Damage();
        }
    }

    protected void Damage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position, raycastSize, 0f, enemyLayer);
            timeBetweenSwings = 0f;

            if (hit && canHit)
            {
                Debug.Log("Hit");
                animator.SetTrigger("isPunching");
                hasHit = true;
                hit.gameObject.SendMessage("RecieveDamage", hitDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    protected void Cooldown()
    {
        timeBetweenSwings += Time.deltaTime;

        if(hitTime <= timeBetweenSwings)
        {
            canHit = true;
        }
        else if (hitTime >= timeBetweenSwings)
        {
            canHit = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, raycastSize);
    }

}
