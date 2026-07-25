using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : GunScript
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector2 raycastSize;

    [SerializeField] float damage;
    bool canHit;

    public override void Shoot()
    {
        var hit = Physics2D.OverlapBox(transform.position, raycastSize, 0f, enemyLayer);

        if ((Input.GetMouseButtonDown(0)) && hit)
        {
            hit.gameObject.SendMessage("RecieveDamage", damage, SendMessageOptions.DontRequireReceiver);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, raycastSize);
    }
}
