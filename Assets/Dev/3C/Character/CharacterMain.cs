using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    private Quaternion direction; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if(GameManager.instance.dataController.direction != Vector3.zero)
        direction = Quaternion.LookRotation(GameManager.instance.dataController.direction);
        else
            direction = Quaternion.identity;


        transform.rotation = Quaternion.Lerp(transform.rotation, direction, Time.deltaTime * 10f); 
    }
}
