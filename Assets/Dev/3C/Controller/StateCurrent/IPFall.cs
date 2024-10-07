using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPFall : IPlayerState
{
    private GS_Fall gs_fall = new GS_Fall(); // Instance de la classe GS_Fall pour g�rer les mouvements sp�cifiques de la chute

    private float ratioT = 0; // Ratio pour la courbe de chute, initialis� � 0

    public void EnterState(ref DataController _dataController)
    {
        // R�initialise le ratioT � 0 lors de l'entr�e dans l'�tat de chute
        ratioT = 0;
    }

    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // G�re la vitesse du joueur en fonction des inputs et des donn�es du ScriptableObject
        GestionSpeed(ref _dataController, _dataScriptable);
        // Calcule la direction du mouvement en fonction des inputs et des donn�es du ScriptableObject
        CalculDirection(ref _dataController, _dataScriptable);
        // G�re la gravit� appliqu�e sur l'axe Y pendant la chute
        GestionGravityY(ref _dataController, _dataScriptable);
        // G�re l'�volution de la courbe de chute
        GestionCurveJump(ref _dataController, _dataScriptable);

        // Met � jour la destination du joueur en fonction de la direction et de la vitesse actuelles
        _dataController.destination += _dataController.direction * _dataController.currentSpeed * Time.fixedDeltaTime;
        // Met � jour la position en Y de la destination en fonction de la gravit� et de la courbe de chute
        _dataController.destination.y += Vector3.up.y * (_dataController.currentGravity * _dataScriptable.curve_Fall.Evaluate(ratioT)) * Time.fixedDeltaTime;
        // V�rifie les collisions avec les murs pendant la chute
        gs_fall.CheckWall(ref _dataController);
        // V�rifie si le joueur doit passer � l'�tat de mouvement
        gs_fall.StateInMove(ref _dataController);
    }

    public void ExitState(ref DataController _dataController)
    {
        // R�initialise le ratioT � 0 lors de la sortie de l'�tat de chute
        ratioT = 0;
    }

    public void ChangeStateByInput(ref DataController _dataController)
    {
        // Actuellement vide, peut �tre utilis�e pour changer d'�tat en fonction des inputs du joueur pendant la chute
    }

    public void ChangeStateByNature(ref DataController _dataController)
    {
        // Change l'�tat en fonction de la physique ou de l'environnement pendant la chute
        gs_fall.StateInMove(ref _dataController);
    }

    private void GestionSpeed(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // D�finit la vitesse cible en fonction des donn�es du ScriptableObject pour la chute
        _dataController.targetSpeed = _dataScriptable.speed_Fall;
        // Interpole la vitesse actuelle vers la vitesse cible en fonction de l'input du joueur
        _dataController.currentSpeed = Mathf.Lerp(_dataController.currentSpeed, _dataController.targetSpeed * GameManager.instance.inputManager.GetInputMove().magnitude, Time.fixedDeltaTime * 3f);
    }

    private void CalculDirection(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // R�cup�re l'input de d�placement du joueur, ajust� par un pourcentage sp�cifique � la chute
        Vector3 inputMove = new Vector3(GameManager.instance.inputManager.GetInputMove().x, 0, GameManager.instance.inputManager.GetInputMove().y) * _dataScriptable.pourcentageMagnitude_Fall;

        // Calcule la direction du mouvement en utilisant Slerp ou Lerp en fonction des param�tres du ScriptableObject pour la chute
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
        // Augmente le ratioT en fonction du temps et du temps de chute sp�cifi� dans le ScriptableObject
        ratioT += Time.fixedDeltaTime / _dataScriptable.time_Fall;
    }

    private void GestionGravityY(ref DataController _dataController, ScriptableObjectController _dataScriptable)
    {
        // D�finit la gravit� actuelle appliqu�e pendant la chute en fonction des donn�es du ScriptableObject
        _dataController.currentGravity = _dataScriptable.graviteY_Fall;
    }
}
