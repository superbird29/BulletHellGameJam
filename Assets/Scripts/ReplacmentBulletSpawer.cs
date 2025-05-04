using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacmentBulletSpawer : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject fireLocation;
    [SerializeField] Vector2 fireDirection; //direction of fire
    [SerializeField] float bulletSpeed; //speed of projectile
    [SerializeField] float rateOfFire; //Less is faster delay in sec
    [SerializeField] float bulletLife; //life of bullets as a float in secs
    [SerializeField] bool pauseFire = true;

    public void StartFiring()
    {
        FireEndlessly();
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

        }
    }

    public void FireEndlessly()
    {
        //Continious fire
        GameObject bul = Instantiate(Bullet, fireLocation.transform.position, Quaternion.identity);
        bul.GetComponent<NewBullet>().UpdateInfo(bulletSpeed, fireDirection);
        StartCoroutine(Delay(rateOfFire));
    }

    public void FireAmounts(int amount, float afterShotDelay)
    {
        StartCoroutine(FireAmount(amount, afterShotDelay));
    }

    //Fire x amount of times
    public IEnumerator FireAmount(int amount, float afterShotDelay)
    {
        pauseFire = false;
        for(int i = 0; i < amount; i++)
        {
            GameObject bul = Instantiate(Bullet, fireLocation.transform.position, Quaternion.identity);
            bul.GetComponent<NewBullet>().UpdateInfo(bulletSpeed, fireDirection);
            yield return new WaitForSeconds(rateOfFire);
        }
        StartCoroutine(Delay(afterShotDelay));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FireEndlessly();
    }
}
