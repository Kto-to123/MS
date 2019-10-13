using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Менеджер вооружения, он отвечает за боевую систему.
//Здесь собранна стрельба и смена оружия
public class Weapon : MonoBehaviour
{
    public WeaponData myWeaponData;

    public static Weapon instance;

    // Основное оружие
    bool mainWeaponInst = false;
    GameObject MainWeaponPrefab;
    WeaponScript mainWeapon;
    ElementMainWeapns mainElements;

    // Метательное оружие
    public bool throwingWeaponActive = false;
    public int throwingAmmunition = 0;
    ElementThrowingWeapons throwingElement;
    //Метательное оружие не имеет своего класа как основное т.к поведение разных типов не сильно отличается.

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

    public void MainWeaponSetActiv(bool v) // Скрытие основного оружия
    {
        if (mainWeaponInst)
        {
            MainWeaponPrefab.SetActive(v);
            mainElements.activ = v;
        }
    }

    public void MainAttack() // Атака основного оружия
    {
        if (mainWeapon != null && mainElements.activ)
        {
            mainWeapon.Attack();
        }
    }

    public void ThrowingAttack() // Бросок метательного оружия
    {
        if (throwingAmmunition > 0 && throwingWeaponActive)
        {
            Instantiate(throwingElement.bullet, throwingElement.fierPoint.position, throwingElement.fierPoint.rotation);
            throwingAmmunition--;
            UIManager.instance.SetAmmo(throwingAmmunition);
            Inventory.instance.throwingWeaponSlot.count = throwingAmmunition;
        }
    }
    
    void DropMainWeapon() // Выбросить основное оружие
    {
        if (mainWeapon != null)
        {
            //Instantiate(mainElements.DropPrefab, mainElements.bowPoint.position, mainElements.bowPoint.rotation);
            mainWeapon = null;
            Destroy(MainWeaponPrefab);
            mainWeaponInst = false;
        }
    }

    public void InstantMainWeapon(int _id) // Включение основного оружия
    {
        DropMainWeapon();
        mainElements = WeaponDataManagerScript.instance.GetElementMainWeapns(_id);
        MainWeaponPrefab = WeaponDataManagerScript.instance.GetMainWeapon(_id);
        MainWeaponPrefab = Instantiate(MainWeaponPrefab, mainElements.bowPoint);
        mainWeapon = MainWeaponPrefab.GetComponent<WeaponScript>();
        mainElements.activ = true;
        mainWeaponInst = true;
    }

    void DropThrowingWeapon() // Выбросить метательное оружие
    {
        if (throwingWeaponActive)
        {
            GameObject drop;
            drop = Instantiate(throwingElement.WeaponModel, throwingElement.fierPoint.position, throwingElement.fierPoint.rotation);
            drop.GetComponent<DropWeapom>().count = throwingAmmunition;
            throwingAmmunition = 0;
        }
    }

    public void InstantWeapon(int _id, int _ammunition) // Включение метательного оружия
    {
        //DropThrowingWeapon();
        throwingElement = WeaponDataManagerScript.instance.GetElementThrowingWeapons(_id);
        throwingWeaponActive = true;
        throwingAmmunition = _ammunition;
        UIManager.instance.SetWeapon(throwingElement.ID, throwingAmmunition);
    }
}
