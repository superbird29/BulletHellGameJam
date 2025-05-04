using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacmentBulletSpawer : MonoBehaviour
{
    GameObject Bullet;
    GameObject fireLocation;
    Vector2 fireDirection; //direction of fire
    float bulletSpeed; //speed of projectile
    float rateOfFire; //Less is faster
    bool pauseFire = true;



    public void StartFiring()
    {
        pauseFire = false;
    }
    public void SwitchBullet(GameObject newbullet)
    {
        Bullet = newbullet;
    }
    public void ChangeRateofFire(int firerate)
    {
        rateOfFire = firerate;
    }
    public void ChangeBulletSpeed(int speed)
    {
        bulletSpeed = speed;
    }

    private void Update()
    {
        if(!pauseFire)
        {
            //Continious fire
            GameObject bul = Instantiate(Bullet, fireLocation.transform.position, Quaternion.identity);
            //bul.GetComponent<NewBullet>().
            
        }
    }

    //Fire x amount of times
    public IEnumerator FireAmount(int amount, float afterShotDelay)
    {
        pauseFire = false;
        for(int i = 0; i < amount; i++)
        {
            GameObject bul = Instantiate(Bullet, fireLocation.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(rateOfFire);
        }
        StartCoroutine(Delay(afterShotDelay));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pauseFire = true;
    }
}
