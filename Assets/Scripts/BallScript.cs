﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Снаряд метательного оружия
public class BallScript : MonoBehaviour
{
    public float speed = 40f;
    Rigidbody rb;
    public int damage = 1;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.velocity = transform.forward * speed;
        Destroy(gameObject, 20f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (other.tag != "Player")
        {
            //Destroy(gameObject);
        }
    }
}
