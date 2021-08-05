using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaRotate : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime*100, 0);
    }
}
