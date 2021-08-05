using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is added for rolling barrels.

public class RollingLeft : MonoBehaviour
{
    private GameManager gameManagerScript;

    public float rollingAngle = 5.0f; // The angular rotation speed
    // private Vector3 rotationAxis = new Vector3(0, 0, 1); // rotation around z-axis
    // Start is called before the first frame update
    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        // Rotation
        if (gameManagerScript.GameIsPlaying)
        {
            transform.Rotate(Vector3.forward, rollingAngle, Space.World);
        }
    }
}
