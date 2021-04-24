using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel : MonoBehaviour
{
    //cache parameters
    [SerializeField]private int totalPoints = 0;
    [SerializeField]private bool isAlive = false;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameLevel>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetPoints()
    {
        return totalPoints;
    }

    public void SetPoints(int point)
    {
        totalPoints = point;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void GameOver()
    {
        isAlive = false;
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        isAlive = true;
        SceneManager.LoadScene("Game");
    }
}
