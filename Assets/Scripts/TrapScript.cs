using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public int damage = 1;
    public GameObject Explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject g = Instantiate(Explosion);
            g.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
