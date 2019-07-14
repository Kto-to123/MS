using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else if (health <= 1)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
