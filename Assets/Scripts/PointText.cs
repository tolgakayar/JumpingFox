using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointText : MonoBehaviour
{
    [SerializeField] Text pointText;
    [SerializeField] Text deletePointText;
    [SerializeField] Text deleteTouchText;
    [SerializeField] int bonusFactor = 20;
    //[SerializeField] int defaultBonusFactor = 20;
    private int totalPoints = 0;
    private float totalNotRounded = 0;

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
            totalNotRounded = bonusFactor * Time.deltaTime;
            totalPoints += Mathf.RoundToInt(totalNotRounded);

            SetPoint();
        }
    }

    public void SetPoint()
    {
        if (!pointText) { return; }

        gameLevel.SetPoints(totalPoints);

        pointText.text = totalPoints.ToString();
        deletePointText.text = totalNotRounded.ToString();
    }

    public void SetPhase(string text)
    {
        deleteTouchText.text = text;
    }
}
