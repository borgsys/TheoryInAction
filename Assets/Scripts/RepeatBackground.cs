using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// INHERITANCE and POLYMORPHISM
public class RepeatBackground : MoveLeft
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    protected override void DoStart()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        base.DoStart();
    }

    // Update is called once per frame
    protected override void DoUpdate()
    {
        base.DoUpdate();
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }

    protected override void DestroyGameObject()
    {
        // Don't destroy anything, this should repeat
    }
}
