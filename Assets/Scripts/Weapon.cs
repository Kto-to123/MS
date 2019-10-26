using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Менеджер вооружения, он отвечает за боевую систему.
//Здесь собранна стрельба и смена оружия
public class Weapon : MonoBehaviour
{
    public WeaponData myWeaponData;
    public static Weapon instance;

    [SerializeField] Transform cameraT;

    // Основное оружие
    bool mainWeaponInst = false;
    GameObject MainWeaponPrefab;
    WeaponScript mainWeapon;
    ElementMainWeapns mainElements;

    // Метательное оружие
    public bool throwingWeaponActive = false;
    public int throwingAmmunition = 0;
    ElementThrowingWeapons throwingElement;
    // Метательное оружие не имеет своего класа как основное т.к поведение разных типов не отличается.

    //Ловушки
    //Разработка системы установки ловушек и копканов
    public GameObject trapGhost;
    GameObject trapGhostInstanse;
    public GameObject trap;


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

    public void TrapSetting()
    {
        Ray ray = new Ray(cameraT.position, cameraT.forward * 10f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, 13, QueryTriggerInteraction.Ignore))
        {
            if (trapGhostInstanse == null)
            {
                trapGhostInstanse = Instantiate<GameObject>(trapGhost);
                trapGhostInstanse.transform.position = hit.point + hit.normal * 0.01f;
                trapGhostInstanse.transform.rotation = Quaternion.LookRotation(-hit.normal);
                //trapGhostInstanse.transform.SetParent(hit.transform);
            }
            else
            {
                trapGhostInstanse.transform.position = hit.point + hit.normal * 0.01f;
                trapGhostInstanse.transform.rotation = Quaternion.LookRotation(-hit.normal);
                //trapGhostInstanse.transform.SetParent(hit.transform);
            }
        }
    }

    public void TrapIstanse()
    {
        if (trapGhostInstanse != null)
        {
            var NewTrap = Instantiate(trap);
            NewTrap.transform.position = trapGhostInstanse.transform.position;
            NewTrap.transform.rotation = trapGhostInstanse.transform.rotation;
            Destroy(trapGhostInstanse);
        }
    }
}
