using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Le GameManager joue un r�le crucial dans la coordination et la gestion des donn�es essentielles du jeu,
    /// particuli�rement en lien avec les trois composants principaux et les inputs,
    /// les inputs
    /// le controller (DataController), 
    /// le character (DataCharacter) (Pas encore disponible), 
    /// la cam�ra (DataCamera).
    /// </summary>
    #region SINGLETON PATTERN
    // Instance statique du GameManager accessible depuis n'importe o� dans le code
    public static GameManager instance;

    private void Awake()
    {
        // V�rifie s'il existe d�j� une instance et si ce n'est pas celle-ci, la d�truit
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

    // R�f�rence � l'InputManager qui g�re les entr�es utilisateur (mouvements, actions, etc.)
    public InputManager inputManager;

    // R�f�rence au DataController qui stocke les donn�es essentielles du contr�leur du joueur
    public DataController dataController;

    // R�f�rence au DataCamera qui contient des informations sur la cam�ra du jeu
    public DataCamera dataCamera;
    public float snap;
    public float forceCollision;
   // public bool stop;


    // Met � jour le DataController avec de nouvelles donn�es
    public void UpdateDataController(DataController _dataController)
    {
        dataController = _dataController;
    }

    // Met � jour le DataCamera avec de nouvelles donn�es
    public void UpdateDataCamera(DataCamera _dataCamera)
    {
        dataCamera = _dataCamera;
    }

    // Renvoie la direction actuelle de la cam�ra stock�e dans DataCamera
    public Vector3 GetDataCameraDirectionCam()
    {
        return dataCamera.directionCam;
    }
}
