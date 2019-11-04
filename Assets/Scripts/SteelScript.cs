using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Мечь. Может использоваться как основное оружие
public class SteelScript : WeaponScript
{
    public double damage = 1;
    public double speedAttack;
    Animator myAnimator;

    private void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        speedAttack = SkillProgress.instance.steelSpeed;
        damage = SkillProgress.instance.steelDamage;
    }

    public override void Attack()
    {
        damage = SkillProgress.instance.steelDamage;
        speedAttack = SkillProgress.instance.steelSpeed;
        myAnimator.SetFloat("SpeedAttacke", (float)speedAttack);
        myAnimator.SetTrigger("Attack1");
    }

    private void OnTriggerEnter(Collider other)
    {
        //myAnimator.animat
        IDamagable enemy = other.GetComponent<IDamagable>();
        if (enemy != null)
        {
            enemy.GetDamage(damage);
        }
    }
}
