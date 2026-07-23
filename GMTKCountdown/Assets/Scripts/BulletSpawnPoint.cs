using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPoint : MonoBehaviour
{

    public void GoToPos(float x, float y)
    {
        transform.localPosition = new Vector2(x, y);
    }
}
