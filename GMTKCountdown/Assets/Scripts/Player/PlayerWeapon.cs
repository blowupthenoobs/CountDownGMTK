using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField] int currentAmmo;
    [SerializeField] int amountOfAmmoToRemove;

    [SerializeField] int maxAmmo;

    [SerializeField] float timeToReload;

    [SerializeField] BulletCounter bulletCounter;
    public float timeBetweenReload;

    bool isMeleeWeapon;
    bool isInRange;
    bool canShoot;

    PlayerMovement playerMovement;
    bool reloadClicked;

    int previousID = -1;

    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        SwitchIDValues();
        FireRate();
        Rotate();
        Melee();
        Reload();
    }

    void FireRate()
    {
        timeBetweenShots += Time.deltaTime;

        if ((Input.GetMouseButtonDown(0)) && canShoot)
        {
            if (fireRate <= timeBetweenShots)
            {
                requestedToShoot = true;
                currentAmmo -= amountOfAmmoToRemove;
                timeBetweenShots = 0;
                bulletCounter.SetAmmo(currentAmmo, maxAmmo);
            }
        }
        else
        {
            requestedToShoot = false;
        }
    }

    void Reload()
    { 
        if(currentAmmo <= 0)
        {
            currentAmmo = 0;
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }

        if(reloadClicked)
        {
           timeBetweenReload += Time.deltaTime;
           canShoot = false;
        }
        if(timeBetweenReload >= timeToReload)
        {
            reloadClicked = false;
            canShoot = true;

            currentAmmo = maxAmmo;
            timeBetweenReload = 0;
            bulletCounter.SetAmmo(currentAmmo, maxAmmo);
        }

        if ((Input.GetKeyDown(KeyCode.R)) && currentAmmo < maxAmmo)
        {
            reloadClicked = true;
        }

        if(playerMovement.newItem)
        {
            currentAmmo = maxAmmo;
            playerMovement.newItem = false;
            bulletCounter.SetAmmo(currentAmmo, maxAmmo);
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

        bool weaponChanged = (ID != previousID);

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

            maxAmmo = 6;
            amountOfAmmoToRemove = 1;
            timeToReload = 0.7f;

            if (requestedToShoot)
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

            maxAmmo = 20;
            amountOfAmmoToRemove = 10;
            timeToReload = 0.85f;

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

            maxAmmo = 10;
            amountOfAmmoToRemove = 1;
            timeToReload = 0.65f;

            if (requestedToShoot)
            {
                Instantiate(bullets[2], bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            }
        }
        if (weaponChanged)
        {
            if (isMeleeWeapon)
            {
                bulletCounter.SetAmmo(0, 0);
            }
            else
            {
                bulletCounter.SetWeapon(ID); 
                bulletCounter.SetAmmo(currentAmmo, maxAmmo);
            }
            previousID = ID;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, raycastSize);
    }
}