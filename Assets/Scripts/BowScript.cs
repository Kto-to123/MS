using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Класс лука, может использоваться как основное оружие
public class BowScript : WeaponScript
{
    public GameObject ActivArrow { get; set; }
    public Transform FierPoint;
    public GameObject arrow;

    ElementMainWeapns Elements;

    public void Start()
    {
        Elements = WeaponDataManagerScript.instance.GetElementMainWeapns(ID);
    }

    public override void Attack()
    {
        Short();
    }

    void Short()
    {
        ElementInventory usebl = WeaponDataManagerScript.instance.GetElementInventory(Inventory.instance.mainAmmunitionSlot.id);
        if (usebl.ammoType == AmmoType.arrow && Inventory.instance.mainAmmunitionSlot.count > 0)
        {
            Inventory.instance.mainAmmunitionSlot.count--;
            Instantiate(Elements.arrow, Elements.bowPoint.position, Elements.bowPoint.rotation);
        }
        
        //Inventory.instance.throwingWeaponSlot.count = throwingAmmunition;
        //UIManager.instance.SetMainAmmo(throwingAmmunition);
    }
}
