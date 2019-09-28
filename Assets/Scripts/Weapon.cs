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

    // Ячейки для лука и стрел
    public int mainAmmo;
    GameObject activArrow;

    //Основное оружие
    WeaponScript mainWeapin;

    
    public Transform fierPoint;
    public GameObject bullet;
    public bool weaponActive = false; // Метательное оружие
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
        // Проверка нажатий клавиш стрельбы
        if (Input.GetButtonDown("Fire1") && weaponActive)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)/* && bow1.activeSelf*/)
        {
            MainAttack();
        }
    }

    private void Start()
    {
        //InstantWeapon(1, 50);
    }

    public void MainWeaponSetActiv(bool v) // Скрытие основного оружия
    {
        mainWeapin.WeaponSetActiv(v);
        //bow1.SetActive(v);
    }

    void MainAttack() // Атака основного оружия
    {
        if (mainWeapin != null)
            mainWeapin.Attack();
    }


    void Shoot() // Бросок метательного оружия
    {
        if (myAmmunition > 0)
        {
            Instantiate(bullet, fierPoint.position, fierPoint.rotation);
            myAmmunition--;
            UIManager.instance.SetAmmo(myAmmunition);
        }
    }
    
    public void DropMainWeapon()
    {

    }

    public void InstantMainWeapon(WeaponScript _weapon, int ammo) // Включение основного оружия
    {
        mainAmmo = ammo;
        mainWeapin = _weapon;
        mainWeapin.InstantiateThis();
    }

    public void DropWeapon() // Выбросить метательное оружие
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

    public void InstantWeapon(int WeaponID, int ammunition) // Включение метательного оружия
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
