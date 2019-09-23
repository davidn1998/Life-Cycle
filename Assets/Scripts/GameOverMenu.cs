using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    //CONGIFGURATION PARAMETERS --------------------------------------------------

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] GameObject newTag;

    //stored values
    string rank = "Zygote";

    public void SetGameOverStats()
    {
        currentScoreText.text = GameManager.Instance.GetCurrentScore().ToString();
        bestScoreText.text = GameManager.Instance.GetBestScore().ToString();
        newTag.SetActive(GameManager.Instance.NewBestScore());
        rankText.text = SetRank();
    }

    //set rank
    private string SetRank()
    {
        int score = GameManager.Instance.GetCurrentScore();

        if (score > 1000)
        {
            rank = "NEWBORN";
        }
        else if (score > 500)
        {
            rank = "FETUS";
        }
        else if (score > 250)
        {
            rank = "EMBRYO";
        }
        else
        {
            rank = "ZYGOTE";
        }

        return rank;
    }

    public void DisplayGameOverScreen()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        GameCanvas.Instance.ToggleUI(false);
        gameOverScreen.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        GameManager.Instance.LoadStartScene();
    }

}
