using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyLeft : MonoBehaviour
{
    public float flySpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Extra flyspeed
        transform.Translate(Vector3.left * Time.deltaTime * flySpeed, Space.World);
    }
}
