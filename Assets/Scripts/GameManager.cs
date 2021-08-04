using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float m_normalSceneSpeed;
    [SerializeField] private float m_highSceneSpeed;
    [SerializeField] private int m_highSpeedScoreMult;
    [SerializeField] protected float m_leftDestroyBoundary = -10;

    //private float m_currentSceneSpeed;
    public static bool firstTime = true;
    private int m_gameScore;
    private int m_caughtBananas;
    private int m_missedBananas;

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
    private bool gameIsReady;
    public bool GameOver { get; private set; } 

    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        if (firstTime)
        {
            // ShowStartInfo
        }
        else
        {
            canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
            SetNoSceneSpeed();
            canvasScript.ShowBigText("GET READY");
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

    // ABSTRACTION
    public void SignalGameOver()
    {
        GameOver = true;
        canvasScript.ShowBigText("GAME OVER!!");
    }
    public void SignalGameReady()
    {
        canvasScript.HideBigText();
        SetNormalSceneSpeed();
        gameIsReady = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

}
