using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Менеджер вооружения, он отвечает за боевую систему.
// Здесь собранна стрельба и смена оружия
public class Weapon : WeaponManager
{
    public WeaponData myWeaponData;

    Transform cameraT;

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

    // Ловушки
    public GameObject trapGhost;
    GameObject trapGhostInstanse;
    public GameObject trap;


    private void Awake()
    {
        cameraT = GetComponentInChildren<Camera>().transform;
    }

    /// <summary>
    /// Установка видимости основного оружия
    /// </summary>
    /// <param name="v"></param>
    public void MainWeaponSetActiv(bool v)
    {
        if (mainWeaponInst)
        {
            MainWeaponPrefab.SetActive(v);
            mainElements.activ = v;
        }
    }

    /// <summary>
    /// Атака основного оружия
    /// </summary>
    public void MainAttack()
    {
        if (mainWeapon != null && mainElements.activ)
        {
            mainWeapon.Attack();
        }
    }

    /// <summary>
    /// Бросок метательного оружия
    /// </summary>
    public void ThrowingAttack()
    {
        if (throwingAmmunition > 0 && throwingWeaponActive)
        {
            Instantiate(throwingElement.bullet, throwingElement.fierPoint.position, throwingElement.fierPoint.rotation);
            throwingAmmunition--;
            UIManager.instance.SetAmmo(throwingAmmunition);
            Inventory.instance.throwingWeaponSlot.count = throwingAmmunition;
        }
    }

    /// <summary>
    /// Выбросить основное оружие
    /// </summary>
    public void DropMainWeapon()
    {
        if (mainWeapon != null)
        {
            //Instantiate(mainElements.DropPrefab, mainElements.bowPoint.position, mainElements.bowPoint.rotation);
            mainWeapon = null;
            Destroy(MainWeaponPrefab);
            mainWeaponInst = false;
        }
    }

    /// <summary>
    /// Включение основного оружия
    /// </summary>
    /// <param name="_id">ID включаемого оружия</param>
    public void InstantMainWeapon(int _id)
    {
        DropMainWeapon();
        mainElements = WeaponDataManagerScript.instance.GetElementMainWeapns(_id);
        MainWeaponPrefab = WeaponDataManagerScript.instance.GetMainWeapon(_id);
        MainWeaponPrefab = Instantiate(MainWeaponPrefab, mainElements.bowPoint);
        mainWeapon = MainWeaponPrefab.GetComponent<WeaponScript>();
        mainElements.activ = true;
        mainWeaponInst = true;
    }

    /// <summary>
    /// Выбросить метательное оружие
    /// </summary>
    public void DropThrowingWeapon()
    {
        if (throwingWeaponActive)
        {
            GameObject drop;
            //drop = Instantiate(throwingElement.WeaponModel, throwingElement.fierPoint.position, throwingElement.fierPoint.rotation);
            //drop.GetComponent<DropWeapom>().count = throwingAmmunition;
            throwingAmmunition = 0;
        }
    }

    /// <summary>
    /// Включение метательного оружия
    /// </summary>
    /// <param name="_id">ID включаемого оружия</param>
    /// <param name="_ammunition">Боеприпасы</param>
    public void InstantWeapon(int _id, int _ammunition)
    {
        //DropThrowingWeapon();
        throwingElement = WeaponDataManagerScript.instance.GetElementThrowingWeapons(_id);
        throwingWeaponActive = true;
        throwingAmmunition = _ammunition;
        UIManager.instance.SetWeapon(throwingElement.ID, throwingAmmunition);
    }

    /// <summary>
    /// Отрисовка оброза ловушки перед установкой
    /// </summary>
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
            }
            else
            {
                trapGhostInstanse.transform.position = hit.point + hit.normal * 0.01f;
                trapGhostInstanse.transform.rotation = Quaternion.LookRotation(-hit.normal);
            }
        }
    }

    /// <summary>
    /// Установка ловушки
    /// </summary>
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
