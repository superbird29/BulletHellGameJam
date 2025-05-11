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
    [SerializeField] float roundDuration = 10f;
    [SerializeField] bool DrawCards = false;


    public void SetRoundTime(float time)
    {
        cardTimer = time;
    }
    public void AdPoints(int point)
    {
        points += point * pointsBouns;
    }

    private void Start()
    {
        StartCoroutine(PointCount());
        StartCoroutine(RoundTime());
    }

    private void Update()
    {
        pointsObj.GetComponent<TMP_Text>().text = "Points: "+ points.ToString();
        cardTimeObj.GetComponent<TMP_Text>().text = "Card Duration: " + cardTimer.ToString("f2");
        if(cardTimer <= 0)
        {
            DrawCards = true;
        }
        if(DrawCards == true)
        {
            DrawCards = false;
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
