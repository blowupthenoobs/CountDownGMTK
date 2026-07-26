using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponControlScript : MonoBehaviour
{
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform gunSpot;
    public GunScript heldWeapon;
    public GunData data;
    private float currentReloadTime;
    private float currentCooldown;

    [SerializeField] BulletCounter bulletCounter;
    [SerializeField] GameObject[] weaponTypes;
    private static GunData heldDataOfWeapon;

    void Awake()
    {
        GameObject recovery = null;

        switch(heldDataOfWeapon?.weapon)
        {
            case GunData.WeaponType.Revolver:
                recovery = Instantiate(weaponTypes[0]);
                break;
            case GunData.WeaponType.Shotgun:
                recovery = Instantiate(weaponTypes[1]);
                break;
            case GunData.WeaponType.Sniper:
                recovery = Instantiate(weaponTypes[2]);
                break;
            case GunData.WeaponType.Sword:
                recovery = Instantiate(weaponTypes[3]);
                break;
            default:
                break;
        }

        if(recovery != null)
        {
            recovery.GetComponent<GunScript>().data = heldDataOfWeapon;
            PickUpWeapon(recovery);
        }
    }

    void Update()
    {
        Rotate();
        if(Input.GetMouseButtonDown(0))
        {
            UseWeapon();
        }

        if(heldWeapon != null)
        {

            if (heldWeapon.data.weapon == GunData.WeaponType.Sword)
            {
                bulletCounter.SetAmmo(0, 0);
            }
            else
            {
                bulletCounter.SetWeapon(heldWeapon.BulletSpriteIndex); // CHANGED - reads from GunScript instead of GunData enum
                bulletCounter.SetAmmo(heldWeapon.data.bulletsInChamber, heldWeapon.data.maxBullets);
            }
        }

        if(heldWeapon == null)
        {
            bulletCounter.SetAmmo(0, 0);
        }

        currentCooldown += Time.deltaTime;

        if(heldWeapon != null)
        {
            if(heldWeapon.data.bulletsInChamber == 0)
            {
                currentReloadTime += Time.deltaTime;

                if(currentReloadTime >= heldWeapon.data.reloadTime)
                {
                    heldWeapon.data.Reload();
                    currentReloadTime = 0;    
                }
            }
        }
        
    }

    void UseWeapon()
    {
        if(heldWeapon != null)
        {
            if(heldWeapon.CanUseWeapon(currentCooldown))
            {
                heldWeapon.Shoot();
                currentCooldown = 0;
            }
        }
        // else
            //Fist code here:
    }

    public void PickUpWeapon(GameObject newWeapon)
    {
        if(heldWeapon != null)
            DropWeapon();
        newWeapon.transform.SetParent(gunSpot);
        newWeapon.transform.position = gunSpot.position;
        newWeapon.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        heldWeapon = newWeapon.GetComponent<GunScript>();
        heldDataOfWeapon = heldWeapon.data;

        currentCooldown = 100;
        currentReloadTime = 0;

        if (heldWeapon.data.weapon == GunData.WeaponType.Sword) // CHANGED - was heldWeapon.data.isMelee
        {
            bulletCounter.SetAmmo(0, 0);
        }
        else
        {
            bulletCounter.SetWeapon((int)heldWeapon.data.weapon);
            bulletCounter.SetAmmo(heldWeapon.data.bulletsInChamber, heldWeapon.data.maxBullets);
        }
    }

    public void DropWeapon()
    {
        if(heldWeapon != null)
        {
            heldWeapon.transform.SetParent(null);
            heldWeapon = null;
            heldDataOfWeapon = null;
        }
    }

    void Rotate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - rotationPoint.position.x, mousePos.y - rotationPoint.position.y).normalized;
        rotationPoint.transform.up = direction;
        // gunSpot.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x));
    }
}
