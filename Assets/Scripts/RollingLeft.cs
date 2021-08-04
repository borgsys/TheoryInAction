using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is added for rolling barrels.

public class RollingLeft : MonoBehaviour
{
    public float rollingAngle = 5.0f; // The angular rotation speed
    // private Vector3 rotationAxis = new Vector3(0, 0, 1); // rotation around z-axis
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        // Rotation
        transform.Rotate(Vector3.forward, rollingAngle, Space.World);
    }
}
