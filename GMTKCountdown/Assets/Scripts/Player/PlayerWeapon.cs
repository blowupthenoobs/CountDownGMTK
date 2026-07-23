using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int ID;

    [SerializeField] float fireRate;
    float timeBetweenShots;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;

    void Update()
    {
        SwitchIDValues();
        FireRate();
    }

    void FireRate()
    {
        timeBetweenShots += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (fireRate <= timeBetweenShots)
            {
                Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                timeBetweenShots = 0;
            }
        }
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
        }
        if(ID == 3)
        {
            fireRate = 0.85f;
        }
        if (ID == 4)
        {
            fireRate = 1f;
        }
    }
}
