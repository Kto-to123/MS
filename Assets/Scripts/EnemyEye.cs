using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    public EnemyNavigations navigations;
    
   

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            navigations.SetDestantion(other.transform);
        }
    }
}
