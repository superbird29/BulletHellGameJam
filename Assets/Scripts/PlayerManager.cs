using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] List<GameObject> HPObj;
    [SerializeField] Sprite emptyHPOBJ;
    [SerializeField] Sprite fullHPOBJ;
    [SerializeField] int Shield = 0;
    [SerializeField] List<GameObject> ShieldOBJ;
    [SerializeField] Sprite ShieldOBJSprite;

    [SerializeField] Rigidbody2D Rigidbody;

    // Update is called once per frame
    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        MovementVertical = Input.GetAxis("Vertical");
        if(HP <= 0)
        {
            //lose
        }
    }

    public void SetHomingTrue(bool homing)
    {
        //set homing true
    }

    public void ChangeLife(int life)
    {
        if(HP == 3 && life > 0)
        {
            return;
        }
        else
        {
            HP += life;
        }
        if(life < 0)
        {
            if(HP >= 0)
                HPObj[HP].GetComponent<Image>().sprite = emptyHPOBJ;
        }

        if(life > 0)
        {
            if (HP <= 3)
                HPObj[HP].GetComponent<Image>().sprite = fullHPOBJ;
        }
    }

    public void ChangeShield(int shield)
    {
        Shield += shield;
        if (shield < 0)
        {
            HPObj[HP].GetComponent<Image>().sprite = emptyHPOBJ;
        }

        if (shield > 0)
        {
            HPObj[HP].GetComponent<Image>().sprite = fullHPOBJ;
        }
    }

    void FixedUpdate()
    {
        Rigidbody.velocity = new Vector2(MovementHorizontal * MoveSpeed, MovementVertical * MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "" )
        {

        }
    }
}
