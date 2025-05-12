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
    [SerializeField] int FireBallCount = 0;
    [SerializeField] List<GameObject> LightingEmitterList;
    [SerializeField] int LightingCount = 0;
    [SerializeField] List<GameObject> IceBallEmitterList;
    [SerializeField] int IceBallCount = 0;


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

    public void ClearWeapons()
    {
        print("Cleared Weapons");
        for (int i = 0; i < IceBallEmitterList.Count; i++)
        {
            IceBallEmitterList[i].SetActive(false);
        }
        for (int j = 0; j < LightingEmitterList.Count; j++)
        {
            LightingEmitterList[j].SetActive(false);
        }
        for (int k = 0; k < FireBallEmitterList.Count; k++)
        {
            FireBallEmitterList[k].SetActive(false);
        }
        FireBallCount = 0;
        LightingCount = 0;
        IceBallCount = 0;
    }

    public void AddHomingIce()
    {
        //set homing true
        print("added ice");
        IceBallEmitterList[IceBallCount].SetActive(true);
        IceBallCount += 1;
    }
    public void AddLaser()
    {
        //set homing true
        print("added Lightning");
        LightingEmitterList[LightingCount].SetActive(true);
        LightingCount += 1;
    }
    public void AddFireBall()
    {
        //set homing true
        print("added Fire");
        FireBallEmitterList[FireBallCount].SetActive(true);
        FireBallCount += 1;
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
