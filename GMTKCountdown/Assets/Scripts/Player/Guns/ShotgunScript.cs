using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : GunScript
{
    public override void Shoot()
    {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(bullet, barrelEnd.transform.position, Quaternion.Euler(0, 0, barrelEnd.transform.eulerAngles.z - 25 + (i*5)));
        }
        data.bulletsInChamber--;
    }

    public override void SetUpGunDetails()
    {
        data = (GunData)ScriptableObject.CreateInstance(typeof(GunData));

        data.weapon = GunData.WeaponType.Shotgun;
        data.cooldownTime = 0.85f;
        data.maxBullets = 2;
        data.reloadTime = 0.85f;
    }
}
