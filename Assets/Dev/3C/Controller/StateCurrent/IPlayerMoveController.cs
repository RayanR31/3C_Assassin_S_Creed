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

        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed , _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.deltaTime * 3f); 

        _dataController.direction = new Vector3(GameManager.instance.inputManager.GetInputMove().x,0, GameManager.instance.inputManager.GetInputMove().y) ;
        
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.deltaTime ;

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
