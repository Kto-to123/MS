using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Отвечает за метательное оружие которое лежит на земле
public class DropWeapom : Drop
{
    public override void Take()
    {
        count = 50;
        Inventory.instance.TakeItem(WeaponDataManagerScript.instance.GetElementThrowingWeapons(id).inventoryID, count);
        //Weapon.instance.InstantWeapon(id, count);
        Destroy(gameObject);
    }
}