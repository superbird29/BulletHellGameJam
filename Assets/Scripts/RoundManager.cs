using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    //Points Info
    [SerializeField] GameObject pointsObj;
    [SerializeField] int points = 0;
    [Tooltip("This Shouldnt go Below 1")]
    [SerializeField] int pointsBouns = 1; //dont go below 1
    [SerializeField] float pointsDelay = .1f;

    //Round Info
    [SerializeField] GameObject cardTimeObj;
    [SerializeField] float cardTimer = 10f;
    // How long the current round will last
    public float roundDuration = 10f;
    [SerializeField] bool DrawCards = false;
    [SerializeField] public GameObject LoseScreen;
    [SerializeField] public GameObject UpgradeScReen;



    public void SetRoundTime(float time)
    {
        cardTimer = time;
    }
    public void AdPoints(int point)
    {
        points += point * pointsBouns;
    }

    //give it the level name 
    public void ChangeScenes(string level)
    {
        GameManager.Instance.SwitchLevel(level);
    }

    private void Start()
    {
        StartCoroutine(PointCount());
        StartCoroutine(RoundTime());
    }

    public void EndRoundRewards()
    {
        print("ended round");
        Time.timeScale = 0;
        GameManager.Instance._DeckManager.GenerateRewards();
        //activate upgrade screen
        //Generate Upgrade Screen
        //Have player chose upgrades
        //close Upgrades
    }

    private void Update()
    {
        pointsObj.GetComponent<TMP_Text>().text = "Points: "+ points.ToString();
        cardTimeObj.GetComponent<TMP_Text>().text = "Card Duration: " + cardTimer.ToString("f2");
        if(cardTimer <= 0)
        {
            DrawCards = true;
        }
        if(DrawCards == true && GameManager.Instance._PlayerManager != null)
        {
            DrawCards = false;
            GameManager.Instance._PlayerManager.ClearWeapons();
            GameManager.Instance._DeckManager.GenerateHand();
            SetRoundTime(roundDuration);
        }
    }



    IEnumerator PointCount()
    {
        yield return new WaitForSeconds(pointsDelay);
        points += 1 * pointsBouns;
        StartCoroutine(PointCount());
    }

    IEnumerator RoundTime()
    {
        yield return new WaitForSeconds(.02f);
        cardTimer -= .02f;
        StartCoroutine(RoundTime());
    }

}
