using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask playerMask;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite downLever;
    [SerializeField] GameObject leverSound;

    bool isInRadius;
    Elevator elevator;

    // Start is called before the first frame update
    void Start()
    {
        elevator = FindAnyObjectByType<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInLeverRange();
    }

    void isPlayerInLeverRange()
    {
        isInRadius = Physics2D.OverlapCircle(transform.position, radius, playerMask);

        if((Input.GetKeyDown(KeyCode.E)) && elevator.hasCompletedObjective && isInRadius)
        {
            Debug.Log("You Have Completed Task Already!");
        }
        else if ((Input.GetKeyDown(KeyCode.E)) && isInRadius)
        {
            elevator.hasCompletedObjective = true;
            spriteRenderer.sprite = downLever;
            Instantiate(leverSound);
            Debug.Log("PlayerHasCompletedObjective");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
