using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : MonoBehaviour
{
    public int id;

    public void Take()
    {
        Weapon.InstantMainWeapon(id);
        Destroy(gameObject);
    }
}
