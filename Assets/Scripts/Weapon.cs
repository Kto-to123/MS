﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Ячейки для оружия
    public GameObject bullet1;
    public Transform fierPoint1;
    public GameObject WeaponModel1;
    public GameObject bullet2;
    public Transform fierPoint2;
    public GameObject WeaponModel2;
    public GameObject bullet3;
    public Transform fierPoint3;
    public GameObject WeaponModel3;

    public Transform fierPoint;
    public GameObject bullet;
    public bool weaponActive = true;
    public int myAmmunition = 0;
    public int myWeaponID = 0;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weaponActive)
        {
            Shoot();
        }    
    }

    private void Start()
    {
        //InstantWeapon(1, 50);
    }

    void Shoot()
    {
        if (myAmmunition > 0)
        {
            Instantiate(bullet, fierPoint.position, fierPoint.rotation);
            myAmmunition--;
        }
    }

    public void InstantWeapon(int WeaponID, int ammunition)//Выбор оружия
    {
        switch (WeaponID)
        {
            case 0:
                weaponActive = false;
                myAmmunition = 0;
                myWeaponID = 0;
                break;
            case 1:
                weaponActive = true;
                bullet = bullet1;
                fierPoint = fierPoint1;
                myAmmunition = ammunition;
                myWeaponID = 1;
                break;
            case 2:
                weaponActive = true;
                bullet = bullet2;
                fierPoint = fierPoint2;
                myAmmunition = ammunition;
                myWeaponID = 2;
                break;
            case 3:
                weaponActive = true;
                bullet = bullet3;
                fierPoint = fierPoint3;
                myAmmunition = ammunition;
                myWeaponID = 3;
                break;
            default:
                weaponActive = false;
                myAmmunition = 0;
                myWeaponID = 0;
                break;
        }
    }
}
