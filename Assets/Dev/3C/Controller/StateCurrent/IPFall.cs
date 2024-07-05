using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPFall : IPlayerState
{
    private GS_Fall gs_fall = new GS_Fall(); // Instance de la classe GS_Fall pour gérer les mouvements spécifiques de la chute

    private float ratioT = 0; // Ratio pour la courbe de chute, initialisé à 0

    public void EnterState(ref DataController _dataController)
    {
        // Réinitialise le ratioT à 0 lors de l'entrée dans l'état de chute
        ratioT = 0;
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Gère la vitesse du joueur en fonction des inputs et des données du ScriptableObject
        GestionSpeed(ref _dataController, _dataScriptable);
        // Calcule la direction du mouvement en fonction des inputs et des données du ScriptableObject
        CalculDirection(ref _dataController, _dataScriptable);
        // Gère la gravité appliquée sur l'axe Y pendant la chute
        GestionGravityY(ref _dataController, _dataScriptable);
        // Gère l'évolution de la courbe de chute
        GestionCurveJump(ref _dataController, _dataScriptable);

        // Met à jour la destination du joueur en fonction de la direction et de la vitesse actuelles
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;
        // Met à jour la position en Y de la destination en fonction de la gravité et de la courbe de chute
        _dataController.destination.y += Vector3.up.y * (_dataController.currentGravity * _dataScriptable.curve_Fall.Evaluate(ratioT)) * Time.fixedDeltaTime;

        // Vérifie les collisions avec les murs pendant la chute
        gs_fall.CheckWall(ref _dataController);
        // Vérifie si le joueur doit passer à l'état de mouvement
        gs_fall.StateInMove(ref _dataController);
    }

    public void ExitState(ref DataController _dataController)
    {
        // Réinitialise le ratioT à 0 lors de la sortie de l'état de chute
        ratioT = 0;
    }

    public void ChangeStateByInput(ref DataController _dataController)
    {
        // Actuellement vide, peut être utilisée pour changer d'état en fonction des inputs du joueur pendant la chute
    }

    public void ChangeStateByNature(ref DataController _dataController)
    {
        // Change l'état en fonction de la physique ou de l'environnement pendant la chute
        gs_fall.StateInMove(ref _dataController);
    }

    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Définit la vitesse cible en fonction des données du ScriptableObject pour la chute
        _dataController.targetSpeed = _dataScriptable.speed_Fall;
        // Interpole la vitesse actuelle vers la vitesse cible en fonction de l'input du joueur
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);
    }

    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Récupère l'input de déplacement du joueur, ajusté par un pourcentage spécifique à la chute
        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x, 0, GameManager.instance.inputManager.GetInputMove().y) * _dataScriptable.pourcentageMagnitude_Fall;

        // Calcule la direction du mouvement en utilisant Slerp ou Lerp en fonction des paramètres du ScriptableObject pour la chute
        if (_dataScriptable.DirectionInSlerp_Fall)
        {
            _dataController.direction = Vector3.Slerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragSlerp_Fall);
        }
        else
        {
            _dataController.direction = Vector3.Lerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragLerp_Fall);
        }
    }

    private void GestionCurveJump(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Augmente le ratioT en fonction du temps et du temps de chute spécifié dans le ScriptableObject
        ratioT += Time.fixedDeltaTime / _dataScriptable.time_Fall;
    }

    private void GestionGravityY(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Définit la gravité actuelle appliquée pendant la chute en fonction des données du ScriptableObject
        _dataController.currentGravity = _dataScriptable.graviteY_Fall;
    }
}
