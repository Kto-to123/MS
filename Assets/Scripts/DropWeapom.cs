using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Отвечает за метательное оружие которое лежит на земле
public class DropWeapom : MonoBehaviour
{
    public int id;
    public int count = 50;

    public void Take()
    {
        Weapon.instance.InstantWeapon(id, count);
        Destroy(gameObject);
    }
}

//Позже унаследовать этот клас от дропа