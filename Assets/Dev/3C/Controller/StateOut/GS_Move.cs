using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GS_Move : GestionState
{
    /// GESTION STATE pour le MOVE
    ///     /// Cette class est l'enfant de la class gestionState
    /// Elle stocke toutes les fonctions qui vont permettre de savoir si oui ou non elles doivent changer d'état
    /// Elles sont appelées directement dans la state concernée
    /// Cela évite d'avoir des states avec des dizaines et des dizaines d'état.
    /// Elle contient également une fonction virtuelle pour gérer les collisions du contrôleur.
    /// Ainsi, les collisions peuvent être modifiées en fonction de l'état si nécessaire. <summary>
    /// </summary>
    /// <param name="_dataController"></param>
    /// 

    /// Si le joueur appuie sur la touche jump alors change d'état en jump
    public void StateInJump(ref DataController _dataController)
    {
        if(GameManager.instance.inputManager.GetInputJump() == true)
        {
            _dataController.targetState = DataController.State.jump ;
            _dataController.changeState = true;
            GameManager.instance.inputManager.CancelInputJump(); 
        }
    }

    /// <summary>
    /// Ne fonctionne pas pour l'instant, l'objectif est de snapper le controller sur le sol, pour l'instant si le raycast ne touche rien alors il passe en fall
    /// </summary>
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
