using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataController", menuName = "Data/ControllerData")]
public class ScriptableObjectController : ScriptableObject
{
    [Header("Move")]
    public float speed_Move;
    public float angularDrag_Move;
    public float pourcentageMagnitude_Move = 1;
    public float graviteY_Move = 1;
    public AnimationCurve curve_Move;

    [Header("Jump")]
    public float speed_Jump;
    public float angularDrag_Jump;
    public float pourcentageMagnitude_Jump = 1;
    public float graviteY_Jump = 1;
    public AnimationCurve curve_Jump;

    [Header("Fall")]
    public float speed_Fall;
    public float angularDrag_Fall;
    public float pourcentageMagnitude_Fall = 1;
    public float graviteY_Fall = 1;
    public AnimationCurve curve_Fall ;
}
