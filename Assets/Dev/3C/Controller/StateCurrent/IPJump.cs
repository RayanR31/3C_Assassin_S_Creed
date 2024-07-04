using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPJump : IPlayerState
{
    private GS_Jump gs_jump = new GS_Jump();

    private float ratioT = 1 ; 
    public void EnterState(ref DataController _dataController)
    {
        ratioT = 1;
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        GestionSpeed(ref _dataController, _dataScriptable);
        CalculDirection(ref _dataController, _dataScriptable);
        GestionGravityY(ref _dataController, _dataScriptable);
        GestionCurveJump(ref _dataController, _dataScriptable);

        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;
        _dataController.destination.y += Vector3.up.y * (_dataController.currentGravity * _dataScriptable.curve_Jump.Evaluate(ratioT)) * Time.fixedDeltaTime;

        gs_jump.CheckWall(ref _dataController, _dataScriptable.speed_Jump);
    }

    public void ExitState(ref DataController _dataController)
    {
        ratioT = 1; 
    }

    public void ChangeStateByInput(ref DataController _dataController)
    {
    }

    public void ChangeStateByNature(ref DataController _dataController)
    {
        gs_jump.StateInFall(ref _dataController , ratioT); 
    }
    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        _dataController.targetSpeed = _dataScriptable.speed_Jump;
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);
    }
    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x, 0, GameManager.instance.inputManager.GetInputMove().y) * _dataScriptable.pourcentageMagnitude_Jump;

        if (_dataScriptable.DirectionInSlerp)
        {
            _dataController.direction = Vector3.Slerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragSlerp_Move);
        }
        else
        {
            _dataController.direction = Vector3.Lerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragLerp_Move);
        }
    }
    private void GestionCurveJump(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        ratioT -= Time.fixedDeltaTime / _dataScriptable.time_Jump;

        // Clamp ratioT pour s'assurer qu'il ne descend pas en dessous de 0
        ratioT = Mathf.Clamp(ratioT, 0f, 1f);
    }
    private void GestionGravityY(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        _dataController.currentGravity = _dataScriptable.force_Jump - (GameManager.instance.inputManager.GetInputMove().magnitude * _dataScriptable.downForce_Jump);
    }

}
