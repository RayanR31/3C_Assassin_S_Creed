using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPMove : IPlayerState
{
    private GS_Move gs_move = new GS_Move(); // Instance de la classe GS_Move pour gérer les mouvements spécifiques

    public void EnterState(ref DataController _dataController)
    {
        // Méthode appelée lors de l'entrée dans cet état. Actuellement vide, peut être utilisée pour initialiser l'état.
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Gère la vitesse du joueur en fonction des inputs et des données du ScriptableObject
        GestionSpeed(ref _dataController, _dataScriptable);
        // Calcule la direction du mouvement en fonction des inputs et des données du ScriptableObject
        CalculDirection(ref _dataController, _dataScriptable);
        // Met à jour la destination du joueur en fonction de la direction et de la vitesse actuelles
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;

        // Vérifie les collisions avec les murs
        gs_move.CheckWall(ref _dataController);
        // Calcule la position en Y du point de destination
        gs_move.CalculPointY(ref _dataController, _dataScriptable);
    }

    public void ExitState(ref DataController _dataController)
    {
        // Méthode appelée lors de la sortie de cet état. Actuellement vide, peut être utilisée pour nettoyer l'état.
    }

    public void ChangeStateByInput(ref DataController _dataController)
    {
        // Change l'état en fonction des inputs du joueur
        gs_move.StateInJump(ref _dataController);
    }

    public void ChangeStateByNature(ref DataController _dataController)
    {
        // Méthode appelée pour changer d'état en fonction de la physique ou de l'environnement. Actuellement vide.
    }

    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Définit la vitesse cible en fonction des données du ScriptableObject
        _dataController.targetSpeed = _dataScriptable.speed_Move;
        // Interpole la vitesse actuelle vers la vitesse cible en fonction de l'input du joueur
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);
    }

    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Récupère l'input de déplacement du joueur
        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x, 0, GameManager.instance.inputManager.GetInputMove().y);

        // Calcule la direction du mouvement en utilisant Slerp ou Lerp en fonction des paramètres du ScriptableObject
        if (_dataScriptable.DirectionInSlerp_Move)
        {
            _dataController.direction = Vector3.Slerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragSlerp_Move);
        }
        else
        {
            _dataController.direction = Vector3.Lerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragLerp_Move);
        }
    }
}
