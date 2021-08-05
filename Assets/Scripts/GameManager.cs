using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float m_normalSceneSpeed;
    [SerializeField] private float m_highSceneSpeed;
    [SerializeField] private int m_highSpeedScoreMult;
    [SerializeField] protected float m_leftDestroyBoundary = -10;
    [SerializeField] private float gravityModifier = 5f;
    //private float m_currentSceneSpeed;
    private static bool firstTime = true;
    private int m_gameScore;
    private int m_caughtBananas;
    private int m_missedBananas;

    private AudioSource mainCameraAudio;
    private CanvasScript canvasScript;

    // ENCAPSULATION
    public float SceneSpeed { get; private set; }
    public float LeftDestroyBoundary
    {
        get
        {
            return m_leftDestroyBoundary;
        }
    }
    public bool GameIsPlaying 
    {
        get
        {
            return (gameIsReady && !GameOver); 
        }
    }
    public int GameScore 
    { 
        get
        {
            return m_gameScore;
        } 
    }
    public bool IsPaused
    {
        get
        {
            return (Time.timeScale == 0);
        }
    }
    private bool gameIsReady;
    public bool GameOver { get; private set; } 

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManager Start");
        mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        canvasScript.HideMainText();
        canvasScript.ShowFirstInfo(false);
        canvasScript.ShowButtons(false);
        SetNoSceneSpeed();
        if (firstTime)
        {
            canvasScript.ShowFirstInfo(true);
            Physics.gravity *= gravityModifier;
            firstTime = false;
            PauseGame();
        }
        else
        {
            ResumeGame();
            canvasScript.ShowMainText("GET READY!");
        }
    }

    public void SetHighSceneSpeed()
    {
        SceneSpeed = m_highSceneSpeed;
    }

    public void SetNormalSceneSpeed()
    {
        SceneSpeed = m_normalSceneSpeed;
    }
    public void SetNoSceneSpeed()
    {
        SceneSpeed = 0;
    }

    // POLYMORPHISM and ABSTRACTION
    public void AddScore(int scoreToAdd)
    {
        if (SceneSpeed == m_highSceneSpeed)
        {
            m_gameScore += (scoreToAdd * m_highSpeedScoreMult);
        }
        else
        {
            m_gameScore += scoreToAdd;
        }
        canvasScript.ShowScore(m_gameScore, m_caughtBananas, m_missedBananas);
    }
    public void AddScore(int bananaScore, bool catchedBanana)
    {
        if (catchedBanana)
        {
            AddScore(bananaScore);
            // Update bananascore
            m_caughtBananas++;
        }
        else
        {
            // Update missed bananascore
            m_missedBananas++;
        }
        canvasScript.ShowScore(m_gameScore, m_caughtBananas, m_missedBananas);
    }

    // ABSTRACTIONS
    public void SignalGameOver()
    {
        GameOver = true;
        canvasScript.ShowMainText(" -- GAME OVER --");
        //PauseGame();
        canvasScript.ShowButtons(true);
        mainCameraAudio.Stop();
    }
    public void SignalGameReady()
    {
        canvasScript.HideMainText();
        SetNormalSceneSpeed();
        gameIsReady = true;
    }

    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        mainCameraAudio.Stop();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        mainCameraAudio.Play();
    }

    public void StartFirstGame()
    {
        canvasScript.ShowFirstInfo(false);
        ResumeGame();
        canvasScript.ShowMainText("GET READY!");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
