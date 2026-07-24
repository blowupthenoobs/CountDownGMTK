using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunScript : MonoBehaviour
{
    public GunData data;
    public GameObject bullet;
    public GameObject barrelEnd;

    public virtual void Awake()
    {
        SetUpGunDetails();
        data.Reload();
    }

    public virtual void Shoot()
    {
        Debug.Log("nothing was set for this weapon");
    }

    public virtual void SetUpGunDetails()
    {
        data = (GunData)ScriptableObject.CreateInstance(typeof(GunData));
    }

    public virtual bool CanUseWeapon(float currentWait)
    {
        if(data.bulletsInChamber > 0 && data.cooldownTime <= currentWait)
            return true;
        return false;
    }
}
