using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Movement
    [SerializeField] float MovementHorizontal;
    [SerializeField] float MovementVertical;
    [SerializeField] float MoveSpeed = 0;
    [SerializeField] float Speed = 5;
    [SerializeField] float Run = 5;
    [SerializeField] float RunForwardMoveBoost = 5;

    //Player Info
    [SerializeField] int HP = 3;
    [SerializeField] int Shield = 0;

    [SerializeField] Rigidbody2D Rigidbody;

    // Update is called once per frame
    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        MovementVertical = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        Rigidbody.velocity = new Vector2(MovementHorizontal * MoveSpeed, MovementVertical * MoveSpeed);
    }
}
