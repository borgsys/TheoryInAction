using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE / POLYMORPHISM / ABSTRACTION
public class MoveLeftAlways : MoveLeft
{
    protected override void DoUpdate()
    {
        DoMoveLeft(m_offsetSpeed);
        base.DoUpdate();
    }
    
}
