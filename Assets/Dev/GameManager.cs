using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static GameManager instance;
    private void Awake()
    {
        //Application.targetFrameRate = 60;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public InputManager inputManager; 
    public DataController dataController; 
    public DataCamera dataCamera ;

    public void UpdateDataController(DataController _dataController)
    {
        dataController = _dataController; 
    }
    public void UpdateDataCamera(DataCamera _dataCamera)
    {
        dataCamera = _dataCamera;
    }
    public Vector3 GetDataCameraDirectionCam()
    {
        return dataCamera.directionCam;
    }
}
