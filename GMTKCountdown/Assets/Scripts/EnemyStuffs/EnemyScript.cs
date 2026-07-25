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
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;

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

    protected void OrbitPlayer(float orbitDistance, float orbitSpeed)
    {
        if(target != null)
        {
            var directionDifference = (transform.position - target.transform.position);
            float currentOrbitDegree = Mathf.Atan2(directionDifference.y, directionDifference.x) * Mathf.Rad2Deg;
            // Debug.Log(currentOrbitDegree);
            float newOrbitDegree = currentOrbitDegree + orbitSpeed * Time.fixedDeltaTime;
            rb.MovePosition((Vector2)target.transform.position + (new Vector2(Mathf.Cos(newOrbitDegree * Mathf.Deg2Rad), Mathf.Sin(newOrbitDegree * Mathf.Deg2Rad)) * orbitDistance));
        }
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

    protected GameObject CastProjectile(GameObject projectile)
    {
        return Instantiate(projectile, transform.position, transform.rotation);
    }

    protected GameObject CastProjectile(GameObject projectile, Quaternion rotation)
    {
        return Instantiate(projectile, transform.position, rotation);
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

    protected void FacePlayer()
    {
        if(target == null)
            return;

        if(target.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
