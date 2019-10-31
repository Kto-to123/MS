using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Молот для врагов, отбрасывает игрока при поподании
public class Hammer : MonoBehaviour
{
    public Transform Enemy;
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.GetDamage(damage);
                Vector3 v = new Vector3(player.transform.position.x - Enemy.position.x, 0, player.transform.position.z - Enemy.position.z);
                player.GetComponent<Rigidbody>().AddForce((v).normalized * 200, ForceMode.Impulse);
            }
        }
    }
}
