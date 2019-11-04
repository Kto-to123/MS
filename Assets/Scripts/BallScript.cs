using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Снаряд метательного оружия
public class BallScript : MonoBehaviour
{
    public float speed = 40f;
    Rigidbody rb;
    public bool extinction = true; // Должен ли придмет исчезать со временем
    public int damage = 1;
    [SerializeField] GameObject explosion;

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
        if (!other.CompareTag("Player"))
        {
            IDamagable enemy = other.GetComponent<IDamagable>();
            if (enemy != null)
            {
                if (explosion != null)
                {
                    var expl = Instantiate(explosion);
                    expl.transform.position = gameObject.transform.position;
                }
                else
                {
                    enemy.GetDamage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
