using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed = 40f;
    Rigidbody rb;
    public bool extinction = true; // Должен ли придмет исчезать со временем
    public int damage = 1;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.velocity = transform.forward * speed;

        if (extinction)
            Destroy(gameObject, 20f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Trigger")
        {  
            rb.velocity = new Vector3(0, 0, 0);
            rb.useGravity = false;

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            damage = 0;
        }
    }
}
