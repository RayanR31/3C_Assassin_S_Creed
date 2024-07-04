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
    public bool DirectionInSlerp_Move ;

    [Header("Jump")]
    public float speed_Jump;
    [Tooltip("Contrôle du joueur dans les airs lors du saut")]
    public float pourcentageMagnitude_Jump = 1;
    [Tooltip("Puissance du saut")]
    public float force_Jump = 1;
    [Tooltip("Baisse la puissance du saut en fonction de la magnitude du joystick")]
    public float downForce_Jump = 1;
    [Tooltip("La durée du saut")]
    public float time_Jump = 1;
    public AnimationCurve curve_Jump;

    [Header("Jump : If calcul direction Slerp or Lerp")]
    public float angularDragSlerp_Jump;
    public float angularDragLerp_Jump;
    public bool DirectionInSlerp_Jump;

    [Header("Fall")]
    public float speed_Fall;
    [Tooltip("Contrôle du joueur dans les airs lors de la chute")]
    public float pourcentageMagnitude_Fall = 1;
    public float graviteY_Fall = 1;
    public float time_Fall = 1;
    public AnimationCurve curve_Fall ;

    [Header("Fall : If calcul direction Slerp or Lerp")]
    public float angularDragSlerp_Fall;
    public float angularDragLerp_Fall;
    public bool DirectionInSlerp_Fall;
}
