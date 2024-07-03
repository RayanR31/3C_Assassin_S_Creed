using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    [SerializeField] private Transform pointCamera; 
    [SerializeField] private Transform cam; 
    [SerializeField] private DataCamera dataCamera; 
    [SerializeField] private bool addCompteur; 
    [SerializeField] private int compteur; 
    [SerializeField] private float timerSecuriteDecalage; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointCamera.position = Vector3.Lerp(pointCamera.position,GameManager.instance.dataController.destination,Time.deltaTime * 3f);

        dataCamera.directionCam = cam.transform.eulerAngles;

        if (addCompteur == false && GameManager.instance.dataController.currentState == DataController.State.jump)
        {
            compteur++;
            addCompteur = true;
        }

        if (GameManager.instance.dataController.currentState != DataController.State.jump)
        {
            addCompteur = false;

            if (GameManager.instance.dataController.currentState == DataController.State.move)
            {
                timerSecuriteDecalage += Time.deltaTime;

                if (timerSecuriteDecalage > 0.5f)
                {
                    compteur = 0;
                    timerSecuriteDecalage = 0;
                }
            }
            else
            {
                timerSecuriteDecalage = 0;
            }
        }

        GameManager.instance.UpdateDataCamera(dataCamera); 
    }
}
