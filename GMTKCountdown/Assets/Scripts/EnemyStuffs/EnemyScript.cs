using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyScript : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected GameObject target;
    [SerializeField] protected LayerMask raycastLayers;
    [SerializeField] protected LayerMask playerLayer;
    protected Vector3 lastSeenPlayerPosition; 
    protected bool sawPlayerLastFrame;

    [SerializeField] protected int health;
    [SerializeField] protected float defaultMoveSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] float damage;

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

    protected void Lunge(Vector2 direction, float power)
    {
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }

    protected void MeleeAttack(float range, float attackWidth, float attackDirection)
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + (new Vector2(Mathf.Cos(attackDirection), Mathf.Sin(attackDirection)) * range / 2), new Vector2(range, attackWidth), attackDirection, Vector2.zero, 0f, playerLayer);
        // Debug.

        if(hit)
            hit.collider.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);

        // Debug.Log(hit.transform);
    }

    protected void CastProjectile(GameObject projectile)
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }

    protected void CastProjectile(GameObject projectile, Quaternion rotation)
    {
        Instantiate(projectile, transform.position, rotation);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            target = collision.gameObject;
        // else
        //     Debug.Log("collided with non-player");
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(target == collision.gameObject)
            target = null;
    }

    public void RecieveDamage(int damage)
    {
        // Debug.Log("took " + damage.ToString() + " damage");
        health -= damage;

        if(health <= 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
