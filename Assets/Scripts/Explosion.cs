using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamagable enemy = other.GetComponent<IDamagable>();
        if (enemy != null)
        {
            if (other.tag == "Enemy")
            {
                enemy.GetDamage(2f);
                //other.GetComponent<Enemy>().TakeDamage(1);
                Destroy(gameObject, 2f);
            }
        }
    }
}
