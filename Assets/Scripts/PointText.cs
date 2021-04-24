using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointText : MonoBehaviour
{
    [SerializeField] Text pointText;
    [SerializeField] int bonusFactor = 20;
    //[SerializeField] int defaultBonusFactor = 20;
    private int totalPoints = 0;

    GameLevel gameLevel;

    void Start()
    {
        totalPoints = 0;
        gameLevel = FindObjectOfType<GameLevel>();
        SetPoint();
    }

    void Update()
    {
        if (!gameLevel)
            gameLevel = FindObjectOfType<GameLevel>();

        if (gameLevel.IsAlive())
        {
            totalPoints += Mathf.RoundToInt(bonusFactor * Time.deltaTime);

            SetPoint();
        }
    }

    public void SetPoint()
    {
        if (!pointText) { return; }

        gameLevel.SetPoints(totalPoints);

        pointText.text = totalPoints.ToString();
    }
}
