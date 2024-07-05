using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPJump : IPlayerState
{
    private GS_Jump gs_jump = new GS_Jump(); // Instance de la classe GS_Jump pour gérer les mouvements spécifiques de saut

    private float ratioT = 1; // Ratio pour la courbe de saut, initialisé à 1

    public void EnterState(ref DataController _dataController)
    {
        // Réinitialise le ratioT à 1 lors de l'entrée dans l'état de saut
        ratioT = 1;
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Gère la vitesse du joueur en fonction des inputs et des données du ScriptableObject
        GestionSpeed(ref _dataController, _dataScriptable);
        // Calcule la direction du mouvement en fonction des inputs et des données du ScriptableObject
        CalculDirection(ref _dataController, _dataScriptable);
        // Gère la gravité appliquée sur l'axe Y pendant le saut
        GestionGravityY(ref _dataController, _dataScriptable);
        // Gère l'évolution de la courbe de saut
        GestionCurveJump(ref _dataController, _dataScriptable);

        // Met à jour la destination du joueur en fonction de la direction et de la vitesse actuelles
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;
        // Met à jour la position en Y de la destination en fonction de la gravité et de la courbe de saut
        _dataController.destination.y += Vector3.up.y * (_dataController.currentGravity * _dataScriptable.curve_Jump.Evaluate(ratioT)) * Time.fixedDeltaTime;

        // Vérifie les collisions avec les murs pendant le saut
        gs_jump.CheckWall(ref _dataController);
    }

    public void ExitState(ref DataController _dataController)
    {
        // Réinitialise le ratioT à 1 lors de la sortie de l'état de saut
        ratioT = 1;
    }

    public void ChangeStateByInput(ref DataController _dataController)
    {
        // Actuellement vide, peut être utilisée pour changer d'état en fonction des inputs du joueur pendant le saut
    }

    public void ChangeStateByNature(ref DataController _dataController)
    {
        // Change l'état en fonction de la physique ou de l'environnement pendant le saut
        gs_jump.StateInFall(ref _dataController, ratioT);
    }

    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Définit la vitesse cible en fonction des données du ScriptableObject pour le saut
        _dataController.targetSpeed = _dataScriptable.speed_Jump;
        // Interpole la vitesse actuelle vers la vitesse cible en fonction de l'input du joueur
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);
    }

    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Récupère l'input de déplacement du joueur, ajusté par un pourcentage spécifique au saut
        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x, 0, GameManager.instance.inputManager.GetInputMove().y) * _dataScriptable.pourcentageMagnitude_Jump;

        // Calcule la direction du mouvement en utilisant Slerp ou Lerp en fonction des paramètres du ScriptableObject pour le saut
        if (_dataScriptable.DirectionInSlerp_Jump)
        {
            _dataController.direction = Vector3.Slerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragSlerp_Jump);
        }
        else
        {
            _dataController.direction = Vector3.Lerp(_dataController.direction, Quaternion.Euler(0, GameManager.instance.dataCamera.directionCam.y, 0) * inputMove, Time.fixedDeltaTime * _dataScriptable.angularDragLerp_Jump);
        }
    }

    private void GestionCurveJump(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Diminue le ratioT en fonction du temps et du temps de saut spécifié dans le ScriptableObject
        ratioT -= Time.fixedDeltaTime / _dataScriptable.time_Jump;
        // Clamp ratioT pour s'assurer qu'il ne descend pas en dessous de 0
        ratioT = Mathf.Clamp(ratioT, 0f, 1f);
    }

    private void GestionGravityY(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Calcule la gravité actuelle appliquée en fonction de la force de saut et de la force descendante ajustée par l'input du joueur
        _dataController.currentGravity = _dataScriptable.force_Jump - (GameManager.instance.inputManager.GetInputMove().magnitude * _dataScriptable.downForce_Jump);
    }

}
