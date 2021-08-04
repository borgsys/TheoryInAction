using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftScore : MoveLeft
{
    //private float currentSceneSpeed;
    //protected float leftBound = -10;
    //protected GameManager gameManagerScript;

    //public float speed = 30;
    //public float superspeed = 40;
    private PlayerController playerControllerScript;
    //public float LeftBound { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        //m_currentSceneSpeed = m_gameManagerScript.SceneSpeed;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); // Find and set the actual PlayerController script
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.GameIsPlaying)
        {
            DoMoveLeft();
        }
        // ScorePoints
        if (gameObject.CompareTag("Obstacle") && (transform.position.x < leftBound))
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Double points
            {
                playerControllerScript.AddDoubleScore();
            }
            else
            {
                playerControllerScript.AddScore();
            }

            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Score") && (transform.position.x < leftBound))
        {
            playerControllerScript.AddMissedBananas();
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Carrier") && (transform.position.x < leftBound))
        {
            Destroy(gameObject);
        }


    }
    // POLYMORPHISM (OVERLOADING), INHERITANCE and ABSTRACTION all in one
    /*virtual protected void DoMoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * gameManagerScript.SceneSpeed, Space.World);
    }
    virtual protected void DoMoveLeft(float speedOffset)
    {
        transform.Translate(Vector3.left * Time.deltaTime * (gameManagerScript.SceneSpeed + speedOffset), Space.World);
    } */

}
