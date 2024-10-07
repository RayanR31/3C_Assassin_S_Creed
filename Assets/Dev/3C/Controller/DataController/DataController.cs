using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DataController 
{
    public float currentSpeed;
    public float targetSpeed;
    public float currentGravity ;
    public Vector3 direction;
    public Vector3 destination;

    public enum State 
    {
        move,
        jump,
        fall
    }

    public State currentState ;
    public State targetState ;
    public bool changeState ;
    // Stocke la normale de la surface en cas de collision, initialis�e vers le bas. Nous la stockons car on ne sait jamais nos raycast perdent leurs cible pendant une frame
    public Vector3 hitNormal ;
}
