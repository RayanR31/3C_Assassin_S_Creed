using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerMoveController : IPlayerState
{
    public void EnterState(ref DataController _dataController)
    {
        
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        if(GameManager.instance.inputManager.GetInputMove() != Vector2.zero)
        {
        }
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
}
