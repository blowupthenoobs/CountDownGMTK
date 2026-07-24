using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControlScript : MonoBehaviour
{
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform gunSpot;
    public GunScript heldWeapon;
    private float currentReloadTime;
    private float currentCooldown;
    
    void Update()
    {
        Rotate();
        if(Input.GetMouseButtonDown(0))
        {
            UseWeapon();
        }

        currentCooldown += Time.deltaTime;

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

        currentCooldown = 100;
        currentReloadTime = 0;
    }

    public void DropWeapon()
    {
        if(heldWeapon != null)
        {
            heldWeapon.transform.SetParent(null);
            heldWeapon = null;
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
