using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    private GameObject Player;
    private Vector3 playerPos;

    private Vector3 newPos;
    private float halfCamHeight;
    private float halfCamWidth;

    private Camera cam;
    private Vector3 oldPos;

    public float followSpeed;
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");

        cam = gameObject.GetComponent<Camera>();
        halfCamHeight = cam.orthographicSize;
        halfCamWidth = halfCamHeight * cam.aspect;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("NodeX"))
        {
            transform.position = new Vector3(oldPos.x, transform.position.y, transform.position.z);
        }
        if(other.CompareTag("NodeY"))
        {
            transform.position = new Vector3(transform.position.x, oldPos.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        oldPos = transform.position;
        playerPos = Player.gameObject.transform.position;

        newPos = Vector3.Lerp(transform.position, playerPos, followSpeed);
        newPos.z = -10f;
        transform.position = newPos;
    }
}
