using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Fall : GestionState
{
   /* private float currentForceCollision;

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
                //Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsWorld[i] * sizes[i], Color.green);
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
                    //Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsUp[1] * 1f, Color.green);
                }
            }
            else
            {
                // Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsUp[0] * 1f, Color.green);
            }
        }
    }*/
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
