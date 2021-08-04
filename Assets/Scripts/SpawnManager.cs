using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public int obstacleIndexMin = 0;
    //public int obstacleIndexMax = 4;

    private GameManager gameManagerScript;

    public GameObject[] obstaclePrefabs; //
    public int obstacleIndex;
    private float spawnPosX = 35f;
    private Vector3 spawnPos = new Vector3(35,0,0);
    public float startDelay = 2f;
    public float spawnRate = 2f;
    private float minSpawnRate = 0.7f;
    private float midSpawnRate = 1.3f;
    private float maxSpawnRate = 2.2f;
    private int expandSpawnAtScore = 15;
    private int expandSpawnWith = 4;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Invoke used instead of InvokeRepeating to set different spawn intervals
        Invoke("SpawnObstacle", spawnRate+startDelay);
    }


    void SpawnObstacle()
    {
        if (gameManagerScript.GameIsPlaying)
        {
            // Wait until certain score to add other spawnobjects and also longer spawnrates
            if ((gameManagerScript.GameScore > expandSpawnAtScore) && (spawnRate > midSpawnRate))
            {
                obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            }
            else
            {
                obstacleIndex = Random.Range(0, obstaclePrefabs.Length-expandSpawnWith);
            }

            // obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            spawnPos = obstaclePrefabs[obstacleIndex].transform.position;
            spawnPos.x = spawnPosX;

            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);

            // Set spawn at different time intervals - harder objects always at longer spawntimes
            if (obstacleIndex < (obstaclePrefabs.Length - expandSpawnWith))
            {
                spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            }
            else
            {
                spawnRate = Random.Range(midSpawnRate, maxSpawnRate);
            }
            
            Invoke("SpawnObstacle", spawnRate);
            
        }
        else if (!gameManagerScript.GameOver)
        {
            // In case SpawnObstacle was invoked and game wasn't ready
            Invoke("SpawnObstacle", spawnRate+startDelay);
        }
    }
}
