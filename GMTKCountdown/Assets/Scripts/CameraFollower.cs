using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    private GameObject Player;
    private Vector3 playerPos;

    private Vector3 newPos;
    
    public float followSpeed;
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        playerPos = Player.gameObject.transform.position;

        newPos = Vector3.Lerp(transform.position, playerPos, followSpeed);
        newPos.z = -10f;
        transform.position = newPos;
    }
}
