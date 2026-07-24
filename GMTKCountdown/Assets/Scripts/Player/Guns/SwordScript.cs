using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : GunScript
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector2 raycastSize;

    public int damage;
    public bool hasHit;
    
    public override void Shoot()
    {
        var hit = Physics2D.OverlapBox(transform.position, raycastSize, 0f, enemyLayer);

        if (hit)
        {
            hasHit = true;
        }
        else
        {
            hasHit = false;
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
