using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Fall : GestionState
{
    /// GESTION STATE pour le FALL
    /// Cette class est l'enfant de la class gestionState
    /// Elle stocke toutes les fonctions qui vont permettre de savoir si oui ou non elles doivent changer d'�tat
    /// Elles sont appel�es directement dans la state concern�e
    /// Cela �vite d'avoir des states avec des dizaines et des dizaines d'�tat.
    /// Elle contient �galement une fonction virtuelle pour g�rer les collisions du contr�leur.
    /// Ainsi, les collisions peuvent �tre modifi�es en fonction de l'�tat si n�cessaire. <summary>
    /// </summary>
    /// <param name="_dataController"></param>
    /// 

    /// Si le raycast tirait de destination vers -Vector3.up touche le sol alors il passe en �tat de move
    public void StateInMove(ref DataController _dataController)
    {
        RaycastHit hit;

        if (Physics.SphereCast(_dataController.destination, 1f, -Vector3.up, out hit, GameManager.instance.colliderSphere * 0.8f, 1 << 0))
        {
            _dataController.targetState = DataController.State.move;
            _dataController.changeState = true;
        }
        else
        {
            Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(_dataController.direction) * -Vector3.up * GameManager.instance.colliderSphere * 0.8f, Color.blue);
        }
    }
}
