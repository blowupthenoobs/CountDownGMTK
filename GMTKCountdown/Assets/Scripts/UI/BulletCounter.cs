using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    [SerializeField] private GameObject bulletIconPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Sprite fullBulletSprite;
    [SerializeField] private Sprite emptyBulletSprite;
    [SerializeField] private Sprite[] fullBulletSpriteList;
    [SerializeField] private Sprite[] emptyBulletSpriteList;
    [SerializeField] private GameObject[] bulletIconPrefabList;

    private int currentWeaponIndex = 0; 

    private List<GameObject> pool = new List<GameObject>();
    private List<Image> poolImages = new List<Image>(); 
    

    public void SetWeapon(int weaponIndex) 
    {
        currentWeaponIndex = weaponIndex;
        Sprite fullSprite = fullBulletSpriteList[currentWeaponIndex]; 
        Sprite emptySprite = emptyBulletSpriteList[currentWeaponIndex]; 
    }

    public void SetAmmo(int current, int max)
    {
        while (pool.Count < max)
        {
            GameObject icon = Instantiate(bulletIconPrefab, container);
            pool.Add(icon);
            poolImages.Add(icon.GetComponent<Image>());
        }

        Sprite fullSprite = fullBulletSpriteList[currentWeaponIndex]; 
        Sprite emptySprite = emptyBulletSpriteList[currentWeaponIndex]; 

        for (int i = 0; i < pool.Count; i++)
        {
            if (i >= max)
            {
                pool[i].SetActive(false);
                continue;
            }
            pool[i].SetActive(true);
            if (emptySprite != null) 
            {
                if (i < current)
                {
                    poolImages[i].sprite = fullSprite; 
                }
                else
                {
                    poolImages[i].sprite = emptySprite; 
                }
            }
            else
            {
                pool[i].SetActive(i < current);
            }
        }
    }
}