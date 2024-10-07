using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPJump : IPlayerState
{
    private GS_Jump gs_jump = new GS_Jump(); // Instance de la classe GS_Jump pour g�rer les mouvements sp�cifiques de saut

    private float ratioT = 1; // Ratio pour la courbe de saut, initialis� � 1

    public void EnterState(ref DataController _dataController)
    {
        // R�initialise le ratioT � 1 lors de l'entr�e dans l'�tat de saut
        ratioT = 1;
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // G�re la vitesse du joueur en fonction des inputs et des donn�es du ScriptableObject
        GestionSpeed(ref _dataController, _dataScriptable);
        // Calcule la direction du mouvement en fonction des inputs et des donn�es du ScriptableObject
        CalculDirection(ref _dataController, _dataScriptable);
        // G�re la gravit� appliqu�e sur l'axe Y pendant le saut
        GestionGravityY(ref _dataController, _dataScriptable);
        // G�re l'�volution de la courbe de saut
        GestionCurveJump(ref _dataController, _dataScriptable);

        // Met � jour la destination du joueur en fonction de la direction et de la vitesse actuelles
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;
        // Met � jour la position en Y de la destination en fonction de la gravit� et de la courbe de saut
        _dataController.destination.y += Vector3.up.y * (_dataController.currentGravity * _dataScriptable.curve_Jump.Evaluate(ratioT)) * Time.fixedDeltaTime;

        // V�rifie les collisions avec les murs pendant le saut
        gs_jump.CheckWall(ref _dataController);
    }

    public void ExitState(ref DataController _dataController)
    {
        // R�initialise le ratioT � 1 lors de la sortie de l'�tat de saut
        ratioT = 1;
    }

    public void ChangeStateByInput(ref DataController _dataController)
    {
        // Actuellement vide, peut �tre utilis�e pour changer d'�tat en fonction des inputs du joueur pendant le saut
    }

    public void ChangeStateByNature(ref DataController _dataController)
    {
        // Change l'�tat en fonction de la physique ou de l'environnement pendant le saut
        gs_jump.StateInFall(ref _dataController, ratioT);
    }

    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // D�finit la vitesse cible en fonction des donn�es du ScriptableObject pour le saut
        _dataController.targetSpeed = _dataScriptable.speed_Jump;
        // Interpole la vitesse actuelle vers la vitesse cible en fonction de l'input du joueur
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);
    }

    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // R�cup�re l'input de d�placement du joueur, ajust� par un pourcentage sp�cifique au saut
        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x, 0, GameManager.instance.inputManager.GetInputMove().y) * _dataScriptable.pourcentageMagnitude_Jump ;

        // Calcule la direction du mouvement en utilisant Slerp ou Lerp en fonction des param�tres du ScriptableObject pour le saut
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
        // Diminue le ratioT en fonction du temps et du temps de saut sp�cifi� dans le ScriptableObject
        ratioT -= Time.fixedDeltaTime / _dataScriptable.time_Jump;
        // Clamp ratioT pour s'assurer qu'il ne descend pas en dessous de 0
        ratioT = Mathf.Clamp(ratioT, 0f, 1f);
    }

    private void GestionGravityY(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // Calcule la gravit� actuelle appliqu�e en fonction de la force de saut et de la force descendante ajust�e par l'input du joueur
        _dataController.currentGravity = _dataScriptable.force_Jump - (GameManager.instance.inputManager.GetInputMove().magnitude * _dataScriptable.downForce_Jump);
    }

}
