using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionState
{
    /// Cette class est le parent des class GS, ils vont servir � v�rifier si oui ou non le controlleur doit changer d'�tat.
    /// Cela �vite d'avoir des states avec des dizaines et dizaines d'�tat.
    /// Elle contient �galement une fonction virtuelle pour g�rer les collisions du contr�leur.
    /// Ainsi, les collisions peuvent �tre modifi�es en fonction de l'�tat si n�cessaire.
    /// <summary>
    /// V�rifie les collisions avec les murs et ajuste la destination en cons�quence.
    /// </summary>
    /// <param name="_dataController">R�f�rence au DataController contenant les informations du joueur.</param>
    /// 
    // La force de l'ajustement de la destination
    public float currentForceCollision;

    // Stocke la direction du raycast pour d�tecter les collisions.
    public Vector3 direction;

    /// <summary>
    /// Vector3 testNormal;
    /// </summary>
    /// <param name="_dataController"></param>

    private void CalculNormalOverlap(ref DataController _dataController)
    {
        // Le centre de la sphère
        Vector3 sphereCenter = _dataController.destination;

        // Le rayon de la sphère
        float sphereRadius = 1f;

        // Le tableau pour stocker les colliders avec lesquels la sphère entre en collision
        Collider[] overlappingColliders = new Collider[10];

        // Appel de OverlapSphereNonAlloc pour obtenir le nombre de colliders que la sphère chevauche
        int numOverlappingColliders = Physics.OverlapSphereNonAlloc(sphereCenter, sphereRadius, overlappingColliders);

        // Parcours du tableau des colliders pour calculer la normale de la surface de collision
        for (int i = 0; i < numOverlappingColliders; i++)
        {
            // Obtenir le collider avec lequel la sphère entre en collision
            Collider collider = overlappingColliders[i];

            // Obtenir le point le plus proche sur le collider par rapport au centre de la sphère
            Vector3 closestPoint = collider.ClosestPoint(sphereCenter);

            // Calculer la normale de la surface en prenant la direction du centre de la sphère vers le point le plus proche
            Vector3 normal = (sphereCenter - closestPoint).normalized;
            currentForceCollision = 30f; // Peut �tre calcul�e dynamiquement en fonction de la vitesse
            _dataController.destination += (normal * GameManager.instance.forceCollision * Time.fixedDeltaTime);

            //Debug.Log("Surface Normal: " + normal);

            while (Vector3.Distance(sphereCenter, closestPoint) <= 0.60f)
            {
                _dataController.direction = Vector3.zero;
                _dataController.currentSpeed = 0;

                if(normal.y <= -0.8f)
                {
                    _dataController.targetState = DataController.State.fall;
                    _dataController.changeState = true;
                    break; 
                }

                //Debug.Log("Distance: " + Vector3.Distance(sphereCenter, closestPoint));
                return;
            }

        }

    }
    public virtual void CheckWall(ref DataController _dataController)
    {
        CalculNormalOverlap(ref _dataController);
    }
}
