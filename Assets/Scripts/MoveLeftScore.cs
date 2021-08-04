using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE and POLYMORPHISM
public class MoveLeftScore : MoveLeft
{
    [SerializeField] private int obstacleScore = 1;

    public void AddMyScore()
    {
        if (gameObject.CompareTag("Obstacle")) 
        { // Not really needed in this game but anyway
            gameManagerScript.AddScore(obstacleScore);
        }
        else if (gameObject.CompareTag("Score"))
        {
            gameManagerScript.AddScore(obstacleScore, true);
        }
    }

    // POLYMORPHISM
    protected override void DestroyGameObject()
    {
        if (transform.position.x < gameManagerScript.LeftDestroyBoundary)
        {
            if (gameObject.CompareTag("Obstacle"))
            {
                gameManagerScript.AddScore(obstacleScore);
            }
            else if (gameObject.CompareTag("Score"))
            {
                gameManagerScript.AddScore(obstacleScore, false);
            }
        }
        base.DestroyGameObject();
    }
}
