using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Fall : GestionState
{
    /// GESTION STATE pour le FALL
    ///     /// Cette class est l'enfant de la class gestionState
    /// Elle stocke toutes les fonctions qui vont permettre de savoir si oui ou non elles doivent changer d'état
    /// Elles sont appelées directement dans la state concernée
    /// Cela évite d'avoir des states avec des dizaines et des dizaines d'état.
    /// Elle contient également une fonction virtuelle pour gérer les collisions du contrôleur.
    /// Ainsi, les collisions peuvent être modifiées en fonction de l'état si nécessaire. <summary>
    /// </summary>
    /// <param name="_dataController"></param>
    /// 

    /// Si le raycast tirait de destination vers -Vector3.up touche le sol alors il passe en état de move
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
