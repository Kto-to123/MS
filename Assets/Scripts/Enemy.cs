using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public Renderer EnemyRender;

    private void Start()
    {
        EnemyRender = GetComponent<Renderer>();
    }

    public void TakeDamage(int damage)
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
