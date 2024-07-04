using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionState
{
    private float currentForceCollision;

    public void CheckWall(ref DataController _dataController, float speed)
    {
        Vector3[] directionsWorld = {
                -Vector3.forward,
               Vector3.forward,
               Vector3.up,
               -Vector3.up,
               Vector3.right,
               Vector3.left};

        float[] sizes = { 1f, 1f, 2f, 1f, 1f, 1f };

        currentForceCollision = (speed / 10) * 15;
        // Parcours chaque direction pour détecter les collisions avec les murs
        for (int i = 0; i < directionsWorld.Length; i++)
        {
            RaycastHit hit;

            // Vérifie s'il y a une collision dans la direction actuelle
            if (Physics.SphereCast(_dataController.destination, 0.5f, Quaternion.LookRotation(_dataController.direction) * directionsWorld[i], out hit, sizes[i], 1 << 0 | 1 << 7))
            {
                _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime); // 15 valeur ok && 1 de distance
            }
            else
            {
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * directionsWorld[i] * sizes[i], Color.green);
            }
        }
    }
}
