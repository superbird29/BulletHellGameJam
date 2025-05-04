using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    float BuleltSpeed = 1;
    Vector2 fireDirection = new Vector2(0, -1); //direction of fire



    void Update()
    {
        transform.position += new Vector3(fireDirection.x + BuleltSpeed, fireDirection.y * BuleltSpeed, 0);
    }
}
