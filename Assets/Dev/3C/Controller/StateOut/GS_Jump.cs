using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Jump : GestionState
{

    /// GESTION STATE pour le JUMP
    ///     /// Cette class est l'enfant de la class gestionState
    /// Elle stocke toutes les fonctions qui vont permettre de savoir si oui ou non elles doivent changer d'état
    /// Elles sont appelées directement dans la state concernée
    /// Cela évite d'avoir des states avec des dizaines et des dizaines d'état.
    /// Elle contient également une fonction virtuelle pour gérer les collisions du contrôleur.
    /// Ainsi, les collisions peuvent être modifiées en fonction de l'état si nécessaire. <summary>
    /// </summary>
    /// <param name="_dataController"></param>
    /// <param name="_ratioT"></param>
    /// 

    /// RatioT correspond à la courbe ; si elle atteint 0, alors le saut se termine.

    public void StateInFall(ref DataController _dataController, float _ratioT)
    {
        if(_ratioT == 0)
        {
            _dataController.targetState = DataController.State.fall;
            _dataController.changeState = true;
        }
    }
}
