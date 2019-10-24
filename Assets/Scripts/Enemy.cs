using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public double health;
    Renderer EnemyRender;

    private void Start()
    {
        EnemyRender = GetComponent<Renderer>();
    }

    public void TakeDamage(double damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else if (health <= 1)
        {
            EnemyRender.material.color = Color.red;
        }
    }
}
