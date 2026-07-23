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
        }
        if(!isInRange)
        {
            canPickup = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.hasPickedUpItem)
        {
            player.hasPickedUpItem = false;
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, claimRange);
    }
}
