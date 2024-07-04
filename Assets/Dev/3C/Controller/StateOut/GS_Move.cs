using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GS_Move : GestionState
{
    public void StateInJump(ref DataController _dataController)
    {
        if(GameManager.instance.inputManager.GetInputJump() == true)
        {
            _dataController.targetState = DataController.State.jump ;
            _dataController.changeState = true;
            GameManager.instance.inputManager.CancelInputJump(); 
        }
    }
}
