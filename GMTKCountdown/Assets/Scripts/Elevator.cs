using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask playerLayer;

    public bool hasCompletedObjective;
    bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInElevatorRange();
    }

    void isPlayerInElevatorRange()
    {
        isInRange = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if ((Input.GetKeyDown(KeyCode.E)) && isInRange && hasCompletedObjective)
        {
            Debug.Log("Player Has Picked To Enter Elevator!");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
