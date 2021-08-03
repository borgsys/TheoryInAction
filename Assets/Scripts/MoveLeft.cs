using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30;
    public float superspeed = 40;
    private PlayerController playerControllerScript;
    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); // Find and set the actual PlayerController script
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameReady && !playerControllerScript.gameOver)
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Speed up
            {
                transform.Translate(Vector3.left * Time.deltaTime * superspeed, Space.World);

            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            }

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
}
