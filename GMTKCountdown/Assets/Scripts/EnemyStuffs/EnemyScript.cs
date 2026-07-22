using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private GameObject target;
    private 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool CanSeePlayer()
    {
        if(target == null)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position - target.transform.position);

        if(hit)
            return true;

        return false;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            target = collision.gameObject;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(target == collision.gameObject)
            target = null;
    }
}
