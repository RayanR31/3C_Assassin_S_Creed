using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GS_Move : GestionState
{
    private float currentForceCollision;

    public override void CheckWall(ref DataController _dataController)
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
            }
            else
            {
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsWorld[i] * sizes[i], Color.green);
            }


            if (Physics.SphereCast(_dataController.destination, 0.5f, Quaternion.LookRotation(_dataController.direction) * directionsUp[0], out hit, 1f, 1 << 0))
            {
                _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime); // 15 valeur ok && 1 de distance

                if (Physics.SphereCast(_dataController.destination, 0.5f, Quaternion.LookRotation(_dataController.direction) * directionsUp[1], out hit, 1f, 1 << 0))
                {
                    _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime); // 15 valeur ok && 1 de distance
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
    }
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
    public void CalculPointY(ref DataController _dataController , ScriptableObjectController _dataScriptable)
    {
        RaycastHit hit;

        if (Physics.SphereCast(_dataController.destination, 1f, Quaternion.LookRotation(_dataController.direction) * -Vector3.up, out hit, 6f, 1 << 0))
        {
            desY = hit.point.y + 0.5f; 
        }
        else
        {
            //Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * -Vector3.up * 1.3f, Color.red);
        }

        _dataController.destination.y += (desY - _dataController.destination.y) * -_dataScriptable.graviteY_Fall * Time.fixedDeltaTime;

    }
}
