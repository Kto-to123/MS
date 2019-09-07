using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapom : MonoBehaviour
{
    //public Inventory inventory;
    //public Weapon weapon;

    public int id;
    public int count = 50;

    //private void Start()
    //{
    //    if (weapon == null)
    //    {
    //        weapon = FindObjectOfType<Weapon>();
    //    }
    //}

    public void Take()
    {
        Weapon.instance.InstantWeapon(id, count);
        //Debug.Log("OK");
        Destroy(gameObject);
    }
}

//Позже унаследовать этот клас от дропа