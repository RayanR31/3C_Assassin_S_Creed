using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionState
{
    private float currentForceCollision;
    private Vector3 hitNormal  = -Vector3.up;
    private Vector3 direction;
    public virtual void CheckWall(ref DataController _dataController)
    {
        Vector3[] directionsWorld = {
               -Vector3.forward,
               Vector3.forward,
               Vector3.up,
               -Vector3.up,
               Vector3.right,
               Vector3.left
        };

        float[] sizes = { 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f };

        currentForceCollision = 15f; //(speed / 10) * 15;
        // Parcours chaque direction pour détecter les collisions avec les murs

        RaycastHit hits;

        if (Physics.SphereCast(_dataController.destination, 0.5f, -Vector3.up, out hits, 4f, 1 << 0))
        {
            hitNormal = hits.normal;
        }

        for (int i = 0; i < directionsWorld.Length; i++)
        {
            RaycastHit hit;

            if (Physics.SphereCast(_dataController.destination, 0.5f, -Vector3.up, out hit, 4f, 1 << 0))
            {
                hitNormal = hit.normal;
            }

            if(hitNormal != Vector3.zero)
            {
                 direction = Quaternion.LookRotation(hitNormal) * directionsWorld[i];
            }
            else
            {
                 direction = directionsWorld[i];
            }

            // Vérifie s'il y a une collision dans la direction actuelle
            if (Physics.SphereCast(_dataController.destination, 0.5f, direction, out hit, sizes[i], 1 << 0))
            {
                _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime); // 15 valeur ok && 1 de distance
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(hitNormal) * directionsWorld[i] * sizes[i], Color.red);
            }
            else
            {
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(hitNormal) * directionsWorld[i] * sizes[i], Color.green);
            }

            // directionsWorld[1] = -Vector3.up
            if (directionsWorld[i] == directionsWorld[1])
            {
                if(hit.collider != null && hit.distance <= 0.35f)
                {
                    _dataController.direction = Vector3.zero; 
                }
            }
        }
    }
}
