using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : GunScript
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector2 raycastSize;

    public int swordDamage;
    EnemyScript enemyScript;
    
    public override void Shoot()
    {
        var hit = Physics2D.OverlapBox(transform.position, raycastSize, 0f, enemyLayer);

        if((Input.GetMouseButtonDown(0)) && hit)
        {
            enemyScript = hit.GetComponent<EnemyScript>();

            if(enemyScript != null )
            {
               enemyScript.RecieveDamage(swordDamage);
            }
        }
    }

    public override void SetUpGunDetails()
    {
        data = (GunData)ScriptableObject.CreateInstance(typeof(GunData));

        data.weapon = GunData.WeaponType.Sword;
        data.cooldownTime = 1f;
    }

    public override bool CanUseWeapon(float currentWait)
    {
        if(data.cooldownTime <= currentWait)
            return true;
        return false;
    }
}
