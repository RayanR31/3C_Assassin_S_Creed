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
    private float desY; 
    private Vector3 hitNormal = Vector3.up;
    private float size = 1.2f;
    public void CalculPointY(ref DataController _dataController , ScriptableObjectController _dataScriptable)
    {
        RaycastHit hit;
        if (Physics.SphereCast(new Vector3(_dataController.destination.x, _dataController.destination.y, _dataController.destination.z), 1f, -Vector3.up/*Quaternion.LookRotation(hitNormal) * -Vector3.forward*/, out hit, size, 1 << 0))
        {
           // desY = hit.point.y + 1f;
            //hitNormal = hit.normal;
            // Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(hitNormal) * -Vector3.forward * size, Color.blue);
            //Debug.DrawRay(_dataController.destination, -Vector3.up * size, Color.blue);
            //_dataController.destination.y += (desY - _dataController.destination.y) * -_dataScriptable.graviteY_Fall * Time.fixedDeltaTime;
            //_dataController.destination.y = desY; //Mathf.Lerp(_dataController.destination.y, desY,Time.fixedDeltaTime * 100f);
            /*if(hit.distance > 0.3f)
            {
                _dataController.destination.y += _dataScriptable.graviteY_Fall * Time.fixedDeltaTime;
            }*/

            //size = Vector3.Distance(hit.point, _dataController.destination);
        }
        else
        {
            //Debug.DrawRay(_dataController.destination, -Vector3.up * size, Color.blue);
            _dataController.targetState = DataController.State.fall;
            _dataController.changeState = true;
        }
    }
}
