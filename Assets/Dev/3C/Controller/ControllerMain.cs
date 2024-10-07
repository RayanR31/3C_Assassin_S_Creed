using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControllerMain : MonoBehaviour
{
    /// <summary>
    /// 3C partie controller : Ce script est le manager qui g�re les �tats du controller.
    /// </summary>

    // dataController : Stocke les variables essentielles du controller (la vitesse actuelle, la destination, la direction)
    [SerializeField] private DataController dataController = new DataController();

    // IPlayerStateArray : Stocke chaque �tat dans une liste
    [SerializeField] private List<IPlayerState> IPlayerStateArray = new List<IPlayerState>();

    // scriptableObjectController : Stocke chaque variable utilis�e pour les calculs du controller (les vitesses � atteindre, les courbes, etc.)
    [SerializeField] private ScriptableObjectController scriptableObjectController;

    // Start is called before the first frame update
    private void Start()
    {
        // Ajoute les interfaces d'�tat � la liste IPlayerStateArray au d�marrage
        InitInterface();
        // Initialise la destination � la position actuelle du joueur au d�marrage
        dataController.destination = transform.position;
        //dataController.hitNormal = -Vector3.up;
    }

    // Update is called once per frame
    private void Update()
    {
        // V�rifie s'il doit changer d'�tat
        ChangeState();

        // La position du gameObject tenant le script se dirige vers la destination en interpolation lin�aire 
        transform.position = Vector3.Lerp(transform.position, dataController.destination, Time.deltaTime * 6f);

        // Met � jour le dataController du GameManager
        GameManager.instance.UpdateDataController(dataController);

        // Affiche un rayon indiquant la direction du joueur
        Debug.DrawRay(dataController.destination, dataController.direction * 4f, Color.blue);
    }

    // FixedUpdate is called at a fixed interval
    private void FixedUpdate()
    {
        // Ex�cute l'�tat actif actuel
        PlayStateCurrent();
    }

    // Initialise les interfaces d'�tat
    private void InitInterface()
    {
        IPlayerStateArray.Add(new IPMove());
        IPlayerStateArray.Add(new IPJump());
        IPlayerStateArray.Add(new IPFall());
    }

    // Ex�cute les fonctions de l'�tat actif actuel
    private void PlayStateCurrent()
    {
        // Met � jour les donn�es de l'�tat actif actuel
        IPlayerStateArray[(int)dataController.currentState].CurrentStateUpdate(ref dataController, scriptableObjectController);
        // V�rifie s'il doit changer d'�tat en fonction des inputs
        IPlayerStateArray[(int)dataController.currentState].ChangeStateByInput(ref dataController);
        // V�rifie s'il doit changer d'�tat en fonction de la physique ou de l'environnement
        IPlayerStateArray[(int)dataController.currentState].ChangeStateByNature(ref dataController);
    }

    // Change l'�tat si n�cessaire
    private void ChangeState()
    {
        // Si un changement d'�tat est n�cessaire
        if (dataController.changeState)
        {
            // Ex�cute la fonction de sortie de l'�tat actuel
            IPlayerStateArray[(int)dataController.currentState].ExitState(ref dataController);
            // Change l'�tat
            dataController.currentState = dataController.targetState;
            // Ex�cute la fonction d'entr�e du nouvel �tat
            IPlayerStateArray[(int)dataController.currentState].EnterState(ref dataController);
            // R�initialise le flag de changement d'�tat
            dataController.changeState = false;
        }
    }
}
