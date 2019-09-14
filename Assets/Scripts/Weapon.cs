using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;

    // Ячейки для метатильного оружия
    public GameObject bullet1;
    public Transform fierPoint1;
    public GameObject WeaponModel1;
    public GameObject bullet2;
    public Transform fierPoint2;
    public GameObject WeaponModel2;
    public GameObject bullet3;
    public Transform fierPoint3;
    public GameObject WeaponModel3;

    public GameObject bow1;
    public GameObject arrow1;
    public int mainAmmo;


    public Transform fierPoint;
    public GameObject bullet;
    public bool weaponActive = true;
    public int myAmmunition = 0;
    public int myWeaponID = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

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
            UIManager.instance.SetAmmo(myAmmunition);
        }
    }

    public void DropWeapon()
    {
        GameObject drop;
        switch (myWeaponID)
        {
            //case 0:
                
            //    break;
            case 1:
                drop = Instantiate(WeaponModel1, fierPoint.position, fierPoint.rotation);
                drop.GetComponent<DropWeapom>().count = myAmmunition;
                break;
            case 2:
                drop = Instantiate(WeaponModel2, fierPoint.position, fierPoint.rotation);
                drop.GetComponent<DropWeapom>().count = myAmmunition;
                break;
            case 3:
                drop = Instantiate(WeaponModel3, fierPoint.position, fierPoint.rotation);
                drop.GetComponent<DropWeapom>().count = myAmmunition;
                break;
            //default:
                
            //    break;
        }
    }

    public void InstantMainWeapon(int ID, int ammo)
    {
        //DropMainWeapon
        switch (ID)
        {
            case 0:
                bow1.SetActive(false);
                break;
            case 1:
                bow1.SetActive(true);
                mainAmmo = ammo;
                break;
            default:
                bow1.SetActive(false);
                break;
        }
    }

    public void InstantWeapon(int WeaponID, int ammunition)//Выбор оружия
    {
        DropWeapon();
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
        UIManager.instance.SetWeapon(myWeaponID, myAmmunition);
    }
}
