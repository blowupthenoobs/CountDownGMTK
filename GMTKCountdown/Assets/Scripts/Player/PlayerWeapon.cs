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

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector2 raycastSize;

    bool isMeleeWeapon;
    bool isInRange;

    void Update()
    {
        SwitchIDValues();
        FireRate();
        Rotate();
        Melee();
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

    void Melee()
    {
        isInRange = Physics2D.OverlapBox(transform.position, raycastSize, 0f, enemyLayer);

        if ((Input.GetMouseButtonDown(0)) && isInRange && isMeleeWeapon)
        {
            Debug.Log("Did damage to enemy!");
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
            isMeleeWeapon = true;
        }
        if(ID == 1)
        {
            spriteRenderer.sprite = weaponSprites[0];
            isMeleeWeapon = true;
            //tbadded
        }
        if(ID == 2)
        {
            bulletSpawnPoint.GoToPos(-0.315f, 1.426f);
            fireRate = 0.5f;
            spriteRenderer.sprite = weaponSprites[1];
            isMeleeWeapon = false;

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
            isMeleeWeapon = false;

            if (requestedToShoot)
            {
                for(int i = 0; i < 10; i++)
                {
                    Instantiate(bullets[1], bulletSpawn.transform.position, Quaternion.Euler(0, 0, bulletSpawn.transform.eulerAngles.z - 25 + (i*5)));
                }   
            }
        }
        if (ID == 4)
        {
            bulletSpawnPoint.GoToPos(-0.16f, 1.89f);
            fireRate = 1f;
            spriteRenderer.sprite = weaponSprites[3];
            isMeleeWeapon = false;

            if (requestedToShoot)
            {
                Instantiate(bullets[2], bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, raycastSize);
    }
}
