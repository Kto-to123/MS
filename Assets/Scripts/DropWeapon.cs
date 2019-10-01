using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Клас метательного оружия которое лежит на земле
public class DropWeapon : MonoBehaviour
{
    public int id;

    public void Take()
    {
        Weapon.instance.InstantMainWeapon(id);
        Destroy(gameObject);
    }
}
