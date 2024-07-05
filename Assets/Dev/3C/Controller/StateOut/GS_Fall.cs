using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Fall : GestionState
{
    public void StateInMove(ref DataController _dataController)
    {
        RaycastHit hit;

        if (Physics.SphereCast(_dataController.destination, 1f, -Vector3.up, out hit, 1.2f, 1 << 0))
        {
            _dataController.targetState = DataController.State.move;
            _dataController.changeState = true;
        }
        else
        {
            //Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * -Vector3.up * 1.3f, Color.red);
        }
    }
}
