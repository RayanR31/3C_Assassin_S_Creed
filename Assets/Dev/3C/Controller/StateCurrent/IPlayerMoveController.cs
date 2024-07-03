using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerMoveController : IPlayerState
{
    GS_Move gs_move = new GS_Move();
    public void EnterState(ref DataController _dataController)
    {
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        InitValue(ref _dataController, _dataScriptable);

        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed , _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);

        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x,0, GameManager.instance.inputManager.GetInputMove().y);

        _dataController.direction = Quaternion.Euler(0,GameManager.instance.dataCamera.directionCam.y,0) * inputMove ;
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;

        gs_move.CheckWall(ref _dataController , _dataScriptable.speed_Move); 
    }

    public void ExitState(ref DataController _dataController)
    {

    }

    public void ChangeStateByInput(ref DataController _dataController)
    {

    }

    public void ChangeStateByNature(ref DataController _dataController)
    {

    }
    private void InitValue(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        _dataController.targetSpeed = _dataScriptable.speed_Move;
    }
}
