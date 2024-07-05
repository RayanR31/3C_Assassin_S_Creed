using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Le GameManager joue un rôle crucial dans la coordination et la gestion des données essentielles du jeu,
    /// particulièrement en lien avec les trois composants principaux et les inputs,
    /// les inputs
    /// le controller (DataController), 
    /// le character (DataCharacter) (Pas encore disponible), 
    /// la caméra (DataCamera).
    /// </summary>
    #region SINGLETON PATTERN
    // Instance statique du GameManager accessible depuis n'importe où dans le code
    public static GameManager instance;

    private void Awake()
    {
        // Vérifie s'il existe déjà une instance et si ce n'est pas celle-ci, la détruit
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Sinon, cette instance devient l'instance GameManager active
            instance = this;
        }
    }
    #endregion

    // Référence à l'InputManager qui gère les entrées utilisateur (mouvements, actions, etc.)
    public InputManager inputManager;

    // Référence au DataController qui stocke les données essentielles du contrôleur du joueur
    public DataController dataController;

    // Référence au DataCamera qui contient des informations sur la caméra du jeu
    public DataCamera dataCamera;

    // Met à jour le DataController avec de nouvelles données
    public void UpdateDataController(DataController _dataController)
    {
        dataController = _dataController;
    }

    // Met à jour le DataCamera avec de nouvelles données
    public void UpdateDataCamera(DataCamera _dataCamera)
    {
        dataCamera = _dataCamera;
    }

    // Renvoie la direction actuelle de la caméra stockée dans DataCamera
    public Vector3 GetDataCameraDirectionCam()
    {
        return dataCamera.directionCam;
    }
}
