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
    [SerializeField] GameObject RoundTimeObj;
    [SerializeField] float roundTimer = 15f;
    [SerializeField] bool RoundEnd = false;


    public void SetRoundTime(float time)
    {
        roundTimer = time;
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
        RoundTimeObj.GetComponent<TMP_Text>().text = "Time Remaining: " + roundTimer.ToString();
        if(roundTimer <= 0)
        {
            RoundEnd = true;
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
        yield return new WaitForSeconds(.01f);
        roundTimer -= .01f;
        StartCoroutine(RoundTime());
    }

}
