using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControllerMain : MonoBehaviour
{
    [SerializeField] private DataController dataController = new DataController();

    [SerializeField] private List<IPlayerState> IPlayerStateArray = new List<IPlayerState>();
    [SerializeField] private ScriptableObjectController scriptableObjectController ;

    // Start is called before the first frame update
    private void Start()
    {
        InitInterface();
        dataController.destination = transform.position;  
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeState();

        transform.position = Vector3.Lerp(transform.position,dataController.destination,Time.deltaTime * 6f);

        GameManager.instance.UpdateDataController(dataController);
    }
    private void FixedUpdate()
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
    private void ChangeState()
    {
        if (dataController.changeState)
        {
            IPlayerStateArray[(int)dataController.currentState].ExitState(ref dataController);
            dataController.currentState = dataController.targetState;
            IPlayerStateArray[(int)dataController.currentState].EnterState(ref dataController);
            dataController.changeState = false;
        }
    }
}
