using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] List<GameObject> ShieldOBJList;
    [SerializeField] GameObject ShieldOBJ;
    [SerializeField] GameObject ShieldSpawnLoc;
    [SerializeField] int Bomb = 0;
    [SerializeField] int Blank = 0;

    [SerializeField] Rigidbody2D Rigidbody;
    [SerializeField] bool CanDamage = true;

    //bullet stuff
    [SerializeField] List<GameObject> FireBallEmitterList;
    [SerializeField] List<GameObject> LightingEmitterList;
    [SerializeField] List<GameObject> IceBallEmitterList;


    // Update is called once per frame
    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        MovementVertical = Input.GetAxis("Vertical");
        if(HP <= 0)
        {
            GameManager.Instance._RoundManager.LoseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SetHomingTrue(bool homing)
    {
        //set homing true
    }

    //used for healing life
    public void ChangeHP(int life)
    {
        //changes add life
        HP += life;
        if (HP <= 3)
            HPObj[HP].GetComponent<Image>().sprite = fullHPOBJ;
        if(HP < 3)
        {
            HP = 3; //this is bad code move along
        }
    }

    //Damage Only
    public void DamagePlayer(int damage)
    {
        // Check for shield, Then if there are shields remove them
        // then if no shields 
        if (Shield > 0)
        {
            //removes the end shield
            Shield += damage;
            print(ShieldOBJList.Count);
            Destroy(ShieldOBJList[ShieldOBJList.Count-1]);
            ShieldOBJList.RemoveAt(ShieldOBJList.Count-1);
            //return;
        }
        else
        {
            HP += damage;
            HPObj[HP-1].GetComponent<Image>().sprite = emptyHPOBJ;
        }


        //if(HP == 3 && life > 0)
        //{
        //    Shield += life;
        //    //Destroy(ShieldOBJList[ShieldOBJList.Count]);
        //    //ShieldOBJList.RemoveAt(ShieldOBJList.Count);
        //    return;
        //}
        //else
        //{
        //    HP += life;
        //}
        //if(life < 0)
        //{
        //    if(HP >= 0)
        //        HPObj[HP].GetComponent<Image>().sprite = emptyHPOBJ;
        //}

        //if(life > 0)
        //{
        //    if (HP <= 3)
        //        HPObj[HP].GetComponent<Image>().sprite = fullHPOBJ;
        //}
    }

    public void GainShield(int shield)
    {
        Shield += shield;
        for(int i = 0; i < shield; i++)
        {
            ShieldOBJList.Add(Instantiate(ShieldOBJ, ShieldSpawnLoc.transform));
        }
    }

    public void GainBomb(int bomb)
    {
        Bomb += bomb;
    }

    public void GainBlank(int blank)
    {
        Blank += blank;
    }

    void FixedUpdate()
    {
        Rigidbody.velocity = new Vector2(MovementHorizontal * MoveSpeed, MovementVertical * MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && CanDamage)
        {
            DamagePlayer(-1);
            //triggure flash and inv
            StartCoroutine(Invincibility());
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Invincibility()
    {
        CanDamage = false;
        yield return new WaitForSeconds(.25f);
        CanDamage = true;
    }

    IEnumerator Flicker()
    {
        float n = 0;
        yield return new WaitUntil(() => n < .5f);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        n += .05f;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        n += .05f;
    }
}
