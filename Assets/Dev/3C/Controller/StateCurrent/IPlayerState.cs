using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    /// <summary>
    /// Méthode appelée lors de l'entrée dans un état. Utilisée pour initialiser l'état.
    /// </summary>
    /// <param name="_dataController">Référence au DataController contenant les informations du joueur.</param>
    public void EnterState(ref DataController _dataController);

    /// <summary>
    /// Méthode appelée pour mettre à jour l'état actuel. Contient la logique de l'état.
    /// </summary>
    /// <param name="_dataController">Référence au DataController contenant les informations du joueur.</param>
    /// <param name="_dataScriptable">Référence au ScriptableObjectController contenant des données supplémentaires pour les calculs.</param>
    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable);

    /// <summary>
    /// Méthode appelée lors de la sortie d'un état. Utilisée pour nettoyer ou réinitialiser des données spécifiques à l'état.
    /// </summary>
    /// <param name="_dataController">Référence au DataController contenant les informations du joueur.</param>
    public void ExitState(ref DataController _dataController);

    /// <summary>
    /// Méthode appelée pour changer d'état en fonction des inputs du joueur.
    /// </summary>
    /// <param name="_dataController">Référence au DataController contenant les informations du joueur.</param>
    public void ChangeStateByInput(ref DataController _dataController);

    /// <summary>
    /// Méthode appelée pour changer d'état en fonction de la physique ou de l'environnement.
    /// </summary>
    /// <param name="_dataController">Référence au DataController contenant les informations du joueur.</param>
    public void ChangeStateByNature(ref DataController _dataController);
}
