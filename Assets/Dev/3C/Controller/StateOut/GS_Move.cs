using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GS_Move : GestionState
{
    //private float currentForceCollision;

   /* public override void CheckWall(ref DataController _dataController)
    {
        Vector3[] directionsWorld = {
                -Vector3.forward,
               Vector3.forward,
               Vector3.right,
               Vector3.left};

        Vector3[] directionsUp = {
               Vector3.up,
               -Vector3.up};

        float[] sizes = { 1f, 1f, 1f, 1f, 1f, 1f };

        currentForceCollision = 15f; //(speed / 10) * 15;
        // Parcours chaque direction pour détecter les collisions avec les murs
        for (int i = 0; i < directionsWorld.Length; i++)
        {
            RaycastHit hit;

            // Vérifie s'il y a une collision dans la direction actuelle
            if (Physics.SphereCast(_dataController.destination, 0.5f, Quaternion.LookRotation(_dataController.direction) * directionsWorld[i], out hit, sizes[i], 1 << 0))
            {
                _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime); // 15 valeur ok && 1 de distance
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsWorld[i] * sizes[i], Color.red);

            }
            else
            {
               Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsWorld[i] * sizes[i], Color.green);
            }


            if (Physics.SphereCast(_dataController.destination, 0.5f, Quaternion.LookRotation(_dataController.direction) * directionsUp[0], out hit, 1f, 1 << 0))
            {
                _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime); // 15 valeur ok && 1 de distance
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsUp[0] * 1f, Color.red);

                if (Physics.SphereCast(_dataController.destination, 0.5f, Quaternion.LookRotation(_dataController.direction) * directionsUp[1], out hit, 1f, 1 << 0))
                {
                    _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime); // 15 valeur ok && 1 de distance
                    Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsUp[1] * 1f, Color.red);

                }
                else
                {
                    Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsUp[1] * 1f, Color.green);
                }
            }
            else
            {
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsUp[0] * 1f, Color.green);
            }
        }
    }*/
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
            Debug.Log("je suis au sol");
        }
        else
        {
            //Debug.DrawRay(_dataController.destination, -Vector3.up * size, Color.blue);
            _dataController.targetState = DataController.State.fall;
            _dataController.changeState = true;
        }
    }
}
