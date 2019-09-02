using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapom : MonoBehaviour
{
    //public Inventory inventory;
    public Weapon weapon;

    public int id;
    public int count = 50;

    public void Take()
    {
        weapon.InstantWeapon(id, count);
        Destroy(gameObject);
    }
}

//Позже унаследовать этот клас от дропа