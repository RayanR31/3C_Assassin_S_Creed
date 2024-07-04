using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPFall : IPlayerState
{
    private GS_Fall gs_fall = new GS_Fall();
    public void EnterState(ref DataController _dataController)
    {

    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {

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
    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        
    }
    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {

    }
}
