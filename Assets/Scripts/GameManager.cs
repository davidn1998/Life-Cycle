using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //STORED VALUES ---------------------------------------------------------------

    private bool isGameOver = false;
    private bool screenShake = true;
    private bool newBestScore = false;
    private int currentScore = 0;
    private int bestScore = 0;

    //SINGLETON -------------------------------------------------------------------

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //LEVEL LOADING -----------------------------------------------------------------

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameCanvas.Instance.ToggleUI(true);
    }

    public void LoadStartScene()
    {
        GameCanvas.Instance.ToggleUI(false);
        GameCanvas.Instance.UpdatePointsText(0);
        SceneManager.LoadScene(0);
        SetGameOver(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //SCREEN SHAKE ----------------------------------------------------------------

    public bool IsScreenShakeOn()
    {
        return screenShake;
    }

    public void ToggleScreenShake()
    {
        screenShake = !screenShake;
    }

    //SCORE - getters and setters -------------------------------------------------

    public void UpdateCurrentScore(int score)
    {
        currentScore = score;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            newBestScore = true;
        }
        else
        {
            newBestScore = false;
        }
    }

    public bool NewBestScore()
    {
        return newBestScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;
    }
}
