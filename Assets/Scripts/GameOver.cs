using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = FindObjectOfType<GameLevel>().GetPoints().ToString();
    }

    void Update()
    {
        score.text = FindObjectOfType<GameLevel>().GetPoints().ToString();
    }

    public void RestartGame()
    {
        FindObjectOfType<GameLevel>().RestartGame();
    }
}
