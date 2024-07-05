using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControllerMain : MonoBehaviour
{
    /// <summary>
    /// 3C partie controller : Ce script est le manager qui gère les états du controller.
    /// </summary>

    // dataController : Stocke les variables essentielles du controller (la vitesse actuelle, la destination, la direction)
    [SerializeField] private DataController dataController = new DataController();

    // IPlayerStateArray : Stocke chaque état dans une liste
    [SerializeField] private List<IPlayerState> IPlayerStateArray = new List<IPlayerState>();

    // scriptableObjectController : Stocke chaque variable utilisée pour les calculs du controller (les vitesses à atteindre, les courbes, etc.)
    [SerializeField] private ScriptableObjectController scriptableObjectController;

    // Start is called before the first frame update
    private void Start()
    {
        // Ajoute les interfaces d'état à la liste IPlayerStateArray au démarrage
        InitInterface();
        // Initialise la destination à la position actuelle du joueur au démarrage
        dataController.destination = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Vérifie s'il doit changer d'état
        ChangeState();

        // La position du gameObject tenant le script se dirige vers la destination en interpolation linéaire 
        transform.position = Vector3.Lerp(transform.position, dataController.destination, Time.deltaTime * 6f);

        // Met à jour le dataController du GameManager
        GameManager.instance.UpdateDataController(dataController);

        // Affiche un rayon indiquant la direction du joueur
        Debug.DrawRay(dataController.destination, dataController.direction * 4f, Color.blue);
    }

    // FixedUpdate is called at a fixed interval
    private void FixedUpdate()
    {
        // Exécute l'état actif actuel
        PlayStateCurrent();
    }

    // Initialise les interfaces d'état
    private void InitInterface()
    {
        IPlayerStateArray.Add(new IPMove());
        IPlayerStateArray.Add(new IPJump());
        IPlayerStateArray.Add(new IPFall());
    }

    // Exécute les fonctions de l'état actif actuel
    private void PlayStateCurrent()
    {
        // Met à jour les données de l'état actif actuel
        IPlayerStateArray[(int)dataController.currentState].CurrentStateUpdate(ref dataController, scriptableObjectController);
        // Vérifie s'il doit changer d'état en fonction des inputs
        IPlayerStateArray[(int)dataController.currentState].ChangeStateByInput(ref dataController);
        // Vérifie s'il doit changer d'état en fonction de la physique ou de l'environnement
        IPlayerStateArray[(int)dataController.currentState].ChangeStateByNature(ref dataController);
    }

    // Change l'état si nécessaire
    private void ChangeState()
    {
        // Si un changement d'état est nécessaire
        if (dataController.changeState)
        {
            // Exécute la fonction de sortie de l'état actuel
            IPlayerStateArray[(int)dataController.currentState].ExitState(ref dataController);
            // Change l'état
            dataController.currentState = dataController.targetState;
            // Exécute la fonction d'entrée du nouvel état
            IPlayerStateArray[(int)dataController.currentState].EnterState(ref dataController);
            // Réinitialise le flag de changement d'état
            dataController.changeState = false;
        }
    }
}
