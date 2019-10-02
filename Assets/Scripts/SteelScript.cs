using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Мечь. Может использоваться как основное оружие
public class SteelScript : WeaponScript
{
    public int damage = 1;

    public override void Attack()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Attack1");
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
