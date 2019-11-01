using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            PlayerControllerScript.instance.StairsSetUsing(true);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControllerScript.instance.StairsSetUsing(false);
        }
    }
}
