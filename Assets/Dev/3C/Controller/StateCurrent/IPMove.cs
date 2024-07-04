using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPMove : IPlayerState
{
    private GS_Move gs_move = new GS_Move();
    public void EnterState(ref DataController _dataController)
    {

    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        GestionSpeed(ref _dataController, _dataScriptable);
        CalculDirection(ref _dataController, _dataScriptable);
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;


        gs_move.CheckWall(ref _dataController);
        gs_move.CalculPointY(ref _dataController, _dataScriptable);

    }

    public void ExitState(ref DataController _dataController)
    {

    }

    public void ChangeStateByInput(ref DataController _dataController)
    {
        gs_move.StateInJump(ref _dataController); 
    }

    public void ChangeStateByNature(ref DataController _dataController)
    {

    }
    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        _dataController.targetSpeed = _dataScriptable.speed_Move;
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);
    }
    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x, 0, GameManager.instance.inputManager.GetInputMove().y);

        if (_dataScriptable.DirectionInSlerp_Move)
        {
            _dataController.direction = Vector3.Slerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragSlerp_Move);
        }
        else
        {
            _dataController.direction = Vector3.Lerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragLerp_Move);
        }
    }
}
