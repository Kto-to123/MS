using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс для метательного оружия. Сейчас не используется
public class ThrowingWeaponsScript : MonoBehaviour
{
    public int ID;
    public int Count { get; set; }

    public GameObject Bullet;
    public static GameObject Model;
    public GameObject firePoint;

    public int damage;
    public int speed;

    public static GameObject GetWeapon(int _ID)
    {
        return Model;
    }
}
