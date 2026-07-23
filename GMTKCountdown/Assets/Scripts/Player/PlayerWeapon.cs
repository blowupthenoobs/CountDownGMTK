using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int ID;

    [SerializeField] float fireRate;
    float timeBetweenShots;

    [SerializeField] GameObject[] bullets;
    [SerializeField] GameObject bulletSpawn;

    [SerializeField] Transform rotationPoint;
    bool requestedToShoot;

    [SerializeField] Sprite[] weaponSprites;
    SpriteRenderer spriteRenderer;

    public BulletSpawnPoint bulletSpawnPoint;

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
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(ID == 0)
        {
            spriteRenderer.sprite = null;
        }
        if(ID == 1)
        {
            spriteRenderer.sprite = weaponSprites[0];
            //tbadded
        }
        if(ID == 2)
        {
            bulletSpawnPoint.GoToPos(-0.315f, 1.426f);
            fireRate = 0.5f;
            spriteRenderer.sprite = weaponSprites[1];

            if(requestedToShoot)
            {
                Instantiate(bullets[0], bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            }
        }
        if(ID == 3)
        {
            bulletSpawnPoint.GoToPos(-0.348f, 1.63f);
            fireRate = 0.85f;
            spriteRenderer.sprite = weaponSprites[2];

            if(requestedToShoot)
            {
                Instantiate(bullets[1], bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            }
        }
        if (ID == 4)
        {
            bulletSpawnPoint.GoToPos(-0.16f, 1.89f);
            fireRate = 1f;
            spriteRenderer.sprite = weaponSprites[3];

            if (requestedToShoot)
            {
                Instantiate(bullets[2], bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            }
        }
    }
}
