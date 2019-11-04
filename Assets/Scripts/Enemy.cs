﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Отвечает за внешнее взаимодействие с объектами врагов
public class Enemy : MonoBehaviour, IDamagable
{
    public double health;
    Renderer EnemyRender;
    WeaponManager Weapon;
    [SerializeField] Transform point;

    private void Start()
    {
        EnemyRender = GetComponent<Renderer>();
        Weapon = GetComponent<WeaponManager>();
        Weapon.InstantMainWeapon(2, point, gameObject.layer);
    }

    public void Attack()
    {
        Weapon.MainAttack();
    }

    public void GetDamage(double _Damage, int armorPenetration)
    {
        health -= _Damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else if (health <= 1)
        {
            EnemyRender.material.color = Color.red;
        }
    }

    public void GetDamage(double _Damage)
    {
        health -= _Damage;

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
