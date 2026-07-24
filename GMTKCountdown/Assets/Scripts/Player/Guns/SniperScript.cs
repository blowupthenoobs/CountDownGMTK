using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : GunScript
{
    public override void Shoot()
    {
        Instantiate(bullet, barrelEnd.transform.position, barrelEnd.transform.rotation);
        data.bulletsInChamber--;
    }

    public override void SetUpGunDetails()
    {
        data = (GunData)ScriptableObject.CreateInstance(typeof(GunData));

        data.weapon = GunData.WeaponType.Sniper;
        data.cooldownTime = 1f;
        data.maxBullets = 10;
        data.reloadTime = 1.5f;
    }
}
