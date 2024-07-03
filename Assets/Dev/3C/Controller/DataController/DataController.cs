using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DataController 
{
    public float currentSpeed;
    public float targetSpeed;
    public Vector3 direction;

    public enum State 
    {
        move,
        jump,
        fall
    }

    public State currentState ;
    public State targetState ;
}
