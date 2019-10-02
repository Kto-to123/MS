using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Отвечает за метательное оружие которое лежит на земле
public class DropWeapom : Drop
{
    public override void Take()
    {
        count = 50;
        Weapon.instance.InstantWeapon(id, count);
        Destroy(gameObject);
    }
}

//Позже унаследовать этот клас от дропа