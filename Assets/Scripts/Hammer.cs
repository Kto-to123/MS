using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Transform Enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.GetComponent<Rigidbody>().AddForce((player.transform.position - Enemy.position).normalized * 300, ForceMode.Impulse);
            }
        }
    }
}
