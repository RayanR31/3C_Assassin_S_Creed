using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Jump : GestionState
{
    public void StateInFall(ref DataController _dataController, float _ratioT)
    {
        if(_ratioT == 0)
        {
            _dataController.targetState = DataController.State.fall;
            _dataController.changeState = true;
        }
    }
}
