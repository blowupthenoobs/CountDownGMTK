using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected GameObject target;
    [SerializeField] protected LayerMask raycastLayers;
    protected Vector3 lastSeenPlayerPosition; 
    protected bool sawPlayerLastFrame;

    [SerializeField] protected float defaultMoveSpeed;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected bool CanSeePlayer()
    {
        if(target == null)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, Mathf.Infinity, raycastLayers);
        Debug.DrawLine(transform.position, hit.transform.position, new Color(0f, 0f, 1f));
        // Debug.DrawLine(transform.position, target.transform.position - transform.position, new Color(1f, 0f, 0f)); //Dunno why this one doesn't work quite right
        // Debug.Log(hit.transform);

        if(hit.collider.gameObject == target)
        {
            lastSeenPlayerPosition = hit.transform.position;   
            sawPlayerLastFrame = true; 
            return true;
        }

        return false;
    }

    protected void RunAtPlayer(float speed)
    {
        var normalizedDirection = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        
        rb.MovePosition(rb.position + normalizedDirection * speed * Time.fixedDeltaTime);
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
