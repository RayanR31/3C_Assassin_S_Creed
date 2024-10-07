using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GS_Move : GestionState
{
    /// GESTION STATE pour le MOVE
    /// Cette class est l'enfant de la class gestionState
    /// Elle stocke toutes les fonctions qui vont permettre de savoir si oui ou non elles doivent changer d'�tat
    /// Elles sont appel�es directement dans la state concern�e
    /// Cela �vite d'avoir des states avec des dizaines et des dizaines d'�tat.
    /// Elle contient �galement une fonction virtuelle pour g�rer les collisions du contr�leur.
    /// Ainsi, les collisions peuvent �tre modifi�es en fonction de l'�tat si n�cessaire. <summary>
    /// </summary>
    /// <param name="_dataController"></param>
    /// 

    /// Si le joueur appuie sur la touche jump alors change d'�tat en jump
    public void StateInJump(ref DataController _dataController)
    {
        if(GameManager.instance.inputManager.GetInputJump() == true)
        {
            _dataController.targetState = DataController.State.jump ;
            _dataController.changeState = true;
            GameManager.instance.inputManager.CancelInputJump(); 
        }
    }

    public float coyoteTime = 0;

    public void CalculNormal(ref DataController _dataController)
    {

        if (_dataController.hitNormal != Vector3.zero && _dataController.hitNormal.y > 0.4f)
        {
            direction = Quaternion.LookRotation(_dataController.hitNormal) * -Vector3.forward;
        }
        else
        {
            //_dataController.hitNormal = new Vector3(0,1,0);
            direction =  -Vector3.up;
        }

        Gizmos.color = Color.red;
        Debug.DrawRay(_dataController.destination, direction * 4f);

        RaycastHit hit;

        // Effectue un SphereCast vers le bas pour d�tecter les collisions avec le sol
        if (Physics.SphereCast(_dataController.destination, 0.5f, direction, out hit, 1.5f, 1 << 0))
        {
            _dataController.hitNormal = hit.normal; // Met � jour la normale de collision

            SnapController(ref _dataController.destination.y, hit.point.y + GameManager.instance.snap);

            coyoteTime = 0; 

        }
        else
        {
            coyoteTime += Time.deltaTime;

            if (coyoteTime >= 0.2f)
            {
                _dataController.targetState = DataController.State.fall;
                _dataController.changeState = true;
            }
        }

    }
    private void SnapController(ref float posInit , float targetPos)
    {
        posInit = targetPos ;
    }
}
