using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : GunScript
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector2 raycastSize;

    [SerializeField] float damage;
    [SerializeField] Animator animator;

    [SerializeField] GameObject swordSwingSound;

    //WeaponControlScript weaponControlScript;
    bool canHit;

    protected override void Awake()
    {
        base.Awake();
        GetComponent<Animator>().enabled = true;
    }

    void Start()
    {
        // animator.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.parent?.parent?.gameObject.GetComponent<WeaponControlScript>() != null)
        {
            Instantiate(swordSwingSound, transform.position, Quaternion.identity);
            animator.SetBool("isAttacking", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public override void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position, raycastSize, 0f, enemyLayer);

            if (hit)
            {
                Debug.Log("hit");
                hit.gameObject.SendMessage("RecieveDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public override void SetUpGunDetails()
    {
        data = (GunData)ScriptableObject.CreateInstance(typeof(GunData));

        data.weapon = GunData.WeaponType.Sword;
        data.cooldownTime = 0.75f;
    }

    public override bool CanUseWeapon(float currentWait)
    {
        if(data.cooldownTime <= currentWait)
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, raycastSize);
    }
}
