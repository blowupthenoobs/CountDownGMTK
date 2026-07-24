using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : ScriptableObject
{
    public enum WeaponType {Shotgun, Sniper, Sword, Revolver};
    public WeaponType weapon;
    public float cooldownTime;
    public float reloadTime;
    public int maxBullets;
    public int bulletsInChamber;

    public void Reload()
    {
        bulletsInChamber = maxBullets;
    }
}
