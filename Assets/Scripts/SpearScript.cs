using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Мечь. Может использоваться как основное оружие
public class SpearScript : WeaponScript
{
    public GameObject spear;
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

    public override void AlternativeAttack()
    {
        PlayerManager.instance.DropMainWeapon();
        Inventory.instance.mainWeaponSlot.itemGameObj.GetComponentInChildren<Text>().text = "";
        Inventory.instance.mainWeaponSlot.id = 0;
        Inventory.instance.UpdateEquipmentSlots(Inventory.instance.mainWeaponSlot);
        Instantiate(spear, transform.position, transform.rotation);
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
