using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataController", menuName = "Data/ControllerData")]
public class ScriptableObjectController : ScriptableObject
{
    [Header("Move")]
    public float speed_Move;

    [Header("Move : If calcul direction Slerp or Lerp")]
    public float angularDragSlerp_Move;
    public float angularDragLerp_Move;
    public bool DirectionInSlerp ;

    [Header("Jump")]
    public float speed_Jump;
    public float angularDrag_Jump;
    public float pourcentageMagnitude_Jump = 1;
    public float force_Jump = 1;
    public float downForce_Jump = 1;
    public float time_Jump = 1;
    public AnimationCurve curve_Jump;

    [Header("Fall")]
    public float speed_Fall;
    public float angularDrag_Fall;
    public float pourcentageMagnitude_Fall = 1;
    public float graviteY_Fall = 1;
    public float time_Fall = 1;
    public AnimationCurve curve_Fall ;
}
