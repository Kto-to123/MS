using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Менеджер вооружения, он отвечает за боевую систему.
// Здесь собранна стрельба и смена оружия
public class Weapon : WeaponManager
{
    public WeaponData myWeaponData;

    Transform cameraT;

    //// Основное оружие
    //bool mainWeaponInst = false;
    //GameObject MainWeaponPrefab;
    //WeaponScript mainWeapon;
    //ElementMainWeapns mainElements;

    //// Метательное оружие
    //public bool throwingWeaponActive = false;
    //public int throwingAmmunition = 0;
    //ElementThrowingWeapons throwingElement;
    //// Метательное оружие не имеет своего класа как основное т.к поведение разных типов не отличается.

    // Ловушки
    public GameObject trapGhost;
    GameObject trapGhostInstanse;
    public GameObject trap;


    private void Awake()
    {
        cameraT = GetComponentInChildren<Camera>().transform;
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
                trapGhostInstanse = Instantiate(trapGhost);
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
