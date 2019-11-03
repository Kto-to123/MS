using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Отвечает за внешнее взаимодействие с объектами врагов
public class Enemy : MonoBehaviour
{
    public double health;
    Renderer EnemyRender;

    private void Start()
    {
        EnemyRender = GetComponent<Renderer>();
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="damage"></param>
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
