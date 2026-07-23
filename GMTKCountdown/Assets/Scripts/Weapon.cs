using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float claimRange;
    [SerializeField] LayerMask playerLayer;

    public bool isInRange;
    public bool canPickup;

    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Claim();
    }

    void Claim()
    {
        isInRange = Physics2D.OverlapCircle(transform.position, claimRange, playerLayer);

        if(isInRange)
        {
            canPickup = true;
            Debug.Log("Can Pick Up" + player.weaponPlayerIsOver);
        }
        if(!isInRange)
        {
            canPickup = false;
            Debug.Log("Can't Pick Up" + player.weaponPlayerIsOver);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, claimRange);
    }
}
