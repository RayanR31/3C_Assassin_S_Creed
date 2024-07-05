using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionState
{
    /// Cette class est le parent des class GS, ils vont servir à vérifier si oui ou non le controlleur doit changer d'état.
    /// Cela évite d'avoir des states avec des dizaines et dizaines d'état.
    /// Elle contient également une fonction virtuelle pour gérer les collisions du contrôleur.
    /// Ainsi, les collisions peuvent être modifiées en fonction de l'état si nécessaire.
    /// <summary>
    /// Vérifie les collisions avec les murs et ajuste la destination en conséquence.
    /// </summary>
    /// <param name="_dataController">Référence au DataController contenant les informations du joueur.</param>
    /// 
    // La force de l'ajustement de la destination
    private float currentForceCollision;
    // Stocke la normale de la surface en cas de collision, initialisée vers le bas. Nous la stockons car on ne sait jamais nos raycast perdent leurs cible pendant une frame
    private Vector3 hitNormal = -Vector3.up;
    // Stocke la direction du raycast pour détecter les collisions.
    private Vector3 direction;

    public virtual void CheckWall(ref DataController _dataController)
    {
        // Un tableau de directions à vérifier pour les collisions.
        Vector3[] directionsWorld = {
        -Vector3.forward,
        Vector3.forward,
        Vector3.up,
        -Vector3.up,
        Vector3.right,
        Vector3.left
    };

        // Définit les tailles pour les SphereCast dans chaque direction
        float[] sizes = { 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f };

        // Définit la force de collision actuelle
        currentForceCollision = 30f; // Peut être calculée dynamiquement en fonction de la vitesse

        RaycastHit hits;

        // Effectue un SphereCast vers le bas pour détecter les collisions avec le sol
        if (Physics.SphereCast(_dataController.destination, 0.5f, -Vector3.up, out hits, 4f, 1 << 0))
        {
            hitNormal = hits.normal; // Met à jour la normale de collision
        }

        // Parcourt chaque direction pour détecter les collisions avec les murs
        for (int i = 0; i < directionsWorld.Length; i++)
        {
            RaycastHit hit;

            // Effectue un SphereCast vers le bas pour détecter les collisions avec le sol
            if (Physics.SphereCast(_dataController.destination, 0.5f, -Vector3.up, out hit, 4f, 1 << 0))
            {
                hitNormal = hit.normal; // Met à jour la normale de collision
            }

            // Calcule la direction du SphereCast en fonction de la normale de collision
            // On ajoute une sécurité au cas ou la normal = Vector3.zero car sinon Unity nous harcèle de message !!!
            if (hitNormal != Vector3.zero)
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
                // Ajuste la destination en fonction de la normale de collision et de la force de collision
                _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime);
                // Dessine un rayon rouge pour indiquer une collision
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(hitNormal) * directionsWorld[i] * sizes[i], Color.red);
            }
            else
            {
                // Dessine un rayon vert pour indiquer l'absence de collision
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(hitNormal) * directionsWorld[i] * sizes[i], Color.green);
            }

            // Vérifie la collision avec le sol (la deuxième direction dans directionsWorld)
            if (directionsWorld[i] == directionsWorld[1])
            {
                // Si une collision est détectée et que la distance est inférieure ou égale à 0.35, arrête le mouvement
                if (hit.collider != null && hit.distance <= 0.35f)
                {
                    _dataController.direction = Vector3.zero;
                }
            }
        }
    }
}
