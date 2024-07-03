using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControllerMain : MonoBehaviour
{
    [SerializeField] private DataController dataController = new DataController();

    [SerializeField] private List<IPlayerState> IPlayerStateArray = new List<IPlayerState>();
    [SerializeField] private ScriptableObjectController scriptableObjectController ;

    /*[SerializeField] private IGestionState gestionState = new GestionState();

    */

    // Start is called before the first frame update
    void Start()
    {
        InitInterface();
    }

    // Update is called once per frame
    void Update()
    {
        PlayStateCurrent();
    }
    private void InitInterface()
    {
        IPlayerStateArray.Add(new IPlayerMoveController());
    }

    private void PlayStateCurrent()
    {
        IPlayerStateArray[(int)dataController.currentState].CurrentStateUpdate(ref dataController, scriptableObjectController);
        IPlayerStateArray[(int)dataController.currentState].ChangeStateByInput(ref dataController);
        IPlayerStateArray[(int)dataController.currentState].ChangeStateByNature(ref dataController);
    }
}
