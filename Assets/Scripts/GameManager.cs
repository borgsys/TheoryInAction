using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float m_normalSceneSpeed;
    [SerializeField] private float m_highSceneSpeed;
    //private float m_currentSceneSpeed;
    private int m_gameScore;
    private int m_caughtBananas;
    private int m_missedBananas;

    public float SceneSpeed { get; private set; }
    // ENCAPSULATING
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
        SetNormalSceneSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHighSceneSpeed()
    {
        SceneSpeed = m_highSceneSpeed;
    }

    public void SetNormalSceneSpeed()
    {
        SceneSpeed = m_normalSceneSpeed;
    }

    public void AddScore(int scoreToAdd)
    {
        m_gameScore += scoreToAdd;
        // Update 
    }
    public void AddScore(int bananaScore, bool missedBanana)
    {
        if (!missedBanana)
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
    }

    public void SignalGameOver()
    {
        GameOver = true;
    }


}
