using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject player;

    List<Transform> playerObject = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        playerObject.Add(playerObject[0]);
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        
    }
}