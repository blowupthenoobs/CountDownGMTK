using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbedEnemyProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float initialHeight;
    [SerializeField] float moveSpeed;
    [SerializeField] float risingSpeed;
    [SerializeField] float fallingSpeed;
    [SerializeField] float fallAccellerationSpeed;
    [SerializeField] float splashRadius;

    private float currentHeight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        fallingSpeed += fallAccellerationSpeed * Time.fixedDeltaTime / 2; //Writing accelleration like this keeps it consistent
        currentHeight += (risingSpeed - fallingSpeed);
        fallingSpeed += fallAccellerationSpeed * Time.fixedDeltaTime / 2;


        rb.MovePosition(rb.position + (Vector2)transform.up * moveSpeed * Time.fixedDeltaTime);

        if(currentHeight <= 0)
            Splash();
    }

    private void Splash()
    {
        //Attack people in circle around it
        Destroy(gameObject);
    }

    public void Initialize(Quaternion rotation, float speed)
    {
        transform.rotation = rotation;
        moveSpeed = speed;
    }

}
