using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{
    //CONGIFGURATION PARAMETERS --------------------------------------------------

    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI pauseKeyText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject mobileButtons;
    [SerializeField] GameObject pauseButton;

    //STORED VALUES ---------------------------------------------------------------

    private bool gamePaused = false;

    //SINGLETON -------------------------------------------------------------------

    private static GameCanvas instance;

    public static GameCanvas Instance { get { return instance; } }

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

    //TOGGLE UI ---------------------------------------------------------------------

    public void ToggleUI(bool active)
    {
        pauseKeyText.gameObject.SetActive(active);
        scoreText.gameObject.SetActive(active);
        ToggleMobilePauseButton(active);
        ToggleMobileUI(active);
    }

    public void ToggleMobileUI(bool active)
    {
#if UNITY_IOS || UNITY_ANDROID
        mobileButtons.SetActive(active);
        pauseKeyText.gameObject.SetActive(false);
#else
        mobileButtons.SetActive(false);
#endif
    }

    public void ToggleMobilePauseButton(bool active)
    {
#if UNITY_IOS || UNITY_ANDROID
        pauseButton.SetActive(active);
#else
        pauseButton.SetActive(false);
#endif
    }





    //PAUSE GAME --------------------------------------------------------------------

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && !GameManager.Instance.IsGameOver())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        else
        {
            gamePaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            pauseKeyText.text = "PAUSE [ESC]";
        }
    }

    public void TogglePause()
    {
        if (gamePaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        gamePaused = true;
        ToggleMobileUI(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        ToggleMobilePauseButton(false);
        pauseKeyText.text = "UNPAUSE [ESC]";
    }

    private void UnPause()
    {
        gamePaused = false;
        ToggleMobileUI(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        ToggleMobilePauseButton(true);
        pauseKeyText.text = "PAUSE [ESC]";
    }

    //SCREEN SHAKE ---------------------------------------------------------------------

    public void ToggleScreenShake()
    {
        GameManager.Instance.ToggleScreenShake();
    }

    //Points Text-----------------------------------------------------------------------------------

    public void UpdatePointsText(int score)
    {
        scoreText.text = score.ToString();
    }

    //Mobile Buttons----------------------------------------------------------------------------
    public void Up()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetVertical(1);
    }

    public void Down()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetVertical(-1);
    }

}
