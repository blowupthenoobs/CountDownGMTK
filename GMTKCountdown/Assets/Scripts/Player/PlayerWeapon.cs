using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int ID;

    [SerializeField] float fireRate;
    float timeBetweenShots;

    [SerializeField] GameObject[] bullets;
    [SerializeField] Transform bulletSpawn;

    [SerializeField] Transform rotationPoint;
    public bool requestedToShoot;

    void Update()
    {
        SwitchIDValues();
        FireRate();
        Rotate();
    }

    void FireRate()
    {
        timeBetweenShots += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (fireRate <= timeBetweenShots)
            {
                requestedToShoot = true;
                timeBetweenShots = 0;
            }
        }
        else
        {
            requestedToShoot = false;
        }
    }

    void Rotate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - rotationPoint.position.x, mousePos.y - rotationPoint.position.y).normalized;
        rotationPoint.transform.up = direction;
    }

    void SwitchIDValues()
    {
        if(ID == 0)
        {

        }
        if(ID == 1)
        {

        }
        if(ID == 2)
        {
            fireRate = 0.5f;

            if(requestedToShoot)
            {
                Instantiate(bullets[0], bulletSpawn.position, bulletSpawn.rotation);
            }
        }
        if(ID == 3)
        {
            fireRate = 0.85f;

            if (requestedToShoot)
            {
                Instantiate(bullets[1], bulletSpawn.position, bulletSpawn.rotation);
            }
        }
        if (ID == 4)
        {
            fireRate = 1f;

            if (requestedToShoot)
            {
                Instantiate(bullets[2], bulletSpawn.position, bulletSpawn.rotation);
            }
        }
    }
}
