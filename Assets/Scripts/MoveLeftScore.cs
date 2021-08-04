using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE and POLYMORPHISM
public class MoveLeftScore : MoveLeft
{
    [SerializeField] private int obstacleScore = 1;

    public void AddMyScore()
    {
        gameManagerScript.AddScore(obstacleScore);
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
                gameManagerScript.AddScore(obstacleScore, true);
            }
        }
        base.DestroyGameObject();
    }
}
