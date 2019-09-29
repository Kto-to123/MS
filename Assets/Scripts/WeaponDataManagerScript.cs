using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас отвечает за хранение информации о снаряжении и выдает данные по ID
public class WeaponDataManagerScript : MonoBehaviour
{
    public static WeaponDataManagerScript instance;

    [SerializeField]
    List<ElementMainWeapns> MainElements = new List<ElementMainWeapns>();

    [SerializeField]
    List<ElementThrowingWeapons> ThrowingElements = new List<ElementThrowingWeapons>();

    public GameObject GetMainWeapon(int _ID)
    {
        return MainElements[_ID].bow;
    }

    public GameObject GetMainDropPrefab(int _ID)
    {
        return MainElements[_ID].DropPrefab;
    }

    public GameObject GetThrowingWeapon(int _ID)
    {
        return ThrowingElements[_ID].bullet;
    }

    public GameObject GetThrowingDropPrefab(int _ID)
    {
        return ThrowingElements[_ID].WeaponModel;
    }

    public ElementMainWeapns GetElementMainWeapns(int _ID)
    {
        return MainElements[_ID];
    }

    public ElementThrowingWeapons GetElementThrowingWeapons(int _ID)
    {
        return ThrowingElements[_ID];
    }

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
}

[System.Serializable]
public struct ElementMainWeapns
{
    public int id;
    public Transform bowPoint;
    public GameObject bow;
    public GameObject DropPrefab;
    public GameObject arrow;
    public Transform BowPoint;
}

[System.Serializable]
public struct ElementThrowingWeapons
{
    public GameObject bullet;
    public Transform fierPoint;
    public GameObject WeaponModel;
}
