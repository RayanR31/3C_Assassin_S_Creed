using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // Update is called once per frame
    void Update()
    {
        CalculDirectionWithNormal();

        animator.SetFloat("speed", GameManager.instance.dataController.currentSpeed);
    }

    private void CalculDirectionWithNormal()
    {
        bool IsMove = GameManager.instance.dataController.currentSpeed > 1f;
//        Debug.Log(GameManager.instance.dataController.hitNormal);
        if (GameManager.instance.dataController.hitNormal != Vector3.zero && GameManager.instance.dataController.direction != Vector3.zero && Mathf.Round(GameManager.instance.dataController.hitNormal.z) >= 0f && Mathf.Round(GameManager.instance.dataController.hitNormal.x) >= 0f)
        {
            // Obtenez la rotation de la normale de la surface
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, GameManager.instance.dataController.hitNormal);

            // Appliquez la direction de base en fonction de la normale
            Vector3 adjustedDirection = surfaceRotation * GameManager.instance.dataController.direction;

            // Effectuez une interpolation douce pour ajuster la rotation en fonction de la normale et de la direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(adjustedDirection, GameManager.instance.dataController.hitNormal), Time.deltaTime * 10f);
        }
        else if (GameManager.instance.dataController.hitNormal == Vector3.zero)
        {
            // Appliquez la direction de base en fonction de la normale
            Vector3 direction = GameManager.instance.dataController.direction;

            // Effectuez une interpolation douce pour ajuster la rotation en fonction de la normale et de la direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
        }
        /*else if (IsMove && GameManager.instance.dataController.direction == Vector3.zero)
        {
            transform.rotation = Quaternion.identity;
        }*/
    }
}
