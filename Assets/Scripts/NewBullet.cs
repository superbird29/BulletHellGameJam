using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    float BuleltSpeed = 1;
    float bulletLife = 1;
    Vector2 fireDirection = new Vector2(0, -1); //direction of fire
    [SerializeField] Rigidbody2D Rigidbody;


    private void Start()
    {
        StartCoroutine(Decay());
    }

    public void UpdateInfo(float bulletSpeed, Vector2 firedirection)
    {
        BuleltSpeed = bulletSpeed;
        fireDirection = firedirection;
    }

    void FixedUpdate()
    {
        Rigidbody.velocity = new Vector2(fireDirection.x * BuleltSpeed, fireDirection.y * BuleltSpeed);
    }

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(this);
    }
}
