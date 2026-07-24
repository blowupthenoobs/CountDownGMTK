using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverScript : GunScript
{
    public override void Shoot()
    {
        Instantiate(bullet, barrelEnd.transform.position, barrelEnd.transform.rotation);
        data.bulletsInChamber--;
    }

    public override void SetUpGunDetails()
    {
        data = (GunData)ScriptableObject.CreateInstance(typeof(GunData));

        data.weapon = GunData.WeaponType.Shotgun;
        data.cooldownTime = 0.5f;
        data.maxBullets = 6;
        data.reloadTime = 0.7f;
    }
}
