using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] string sceneName;
    public bool hasCompletedObjective;

    Level5Manager level5Manager;
    bool isInRange;

    [SerializeField] bool isNormalElevator;
    [SerializeField] bool isLevel5Elevator;

    // Start is called before the first frame update
    void Start()
    {
        if(level5Manager == null)
        {
            level5Manager = FindFirstObjectByType<Level5Manager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInElevatorRange();
    }

    void isPlayerInElevatorRange()
    {
        isInRange = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if ((Input.GetKeyDown(KeyCode.E)) && isInRange && !hasCompletedObjective)
        {
            Debug.Log("Need to Complete Objective!");
        }

        if(isNormalElevator)
        {
           if((Input.GetKeyDown(KeyCode.E)) && isInRange && hasCompletedObjective)
           {
               SceneManager.LoadScene(sceneName);
           }
        }
        if(isLevel5Elevator )
        {
            if ((Input.GetKeyDown(KeyCode.E)) && isInRange && level5Manager.enemiesDead)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
