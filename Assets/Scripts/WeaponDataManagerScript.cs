using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        return MainElements[_ID].mainPrefab;
    }

    public GameObject GetMainDropPrefab(int _ID)
    {
        return MainElements[_ID].DropPrefab;
    }

    public ElementMainWeapns GetElementMainWeapns(int _ID)
    {
        return MainElements[_ID];
    }

    public GameObject GetThrowingWeapon(int _ID)
    {
        return ThrowingElements[_ID].bullet;
    }

    public GameObject GetThrowingDropPrefab(int _ID)
    {
        return ThrowingElements[_ID].WeaponModel;
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
    [FormerlySerializedAs("ID")]
    public int ID;
    [FormerlySerializedAs("Activ")]
    public bool activ;
    public Transform bowPoint;
    public GameObject mainPrefab;
    public GameObject DropPrefab;
    public GameObject arrow;
    public Transform fierPoint;
}

[System.Serializable]
public struct ElementThrowingWeapons
{
    public int ID;
    public GameObject bullet;
    public Transform fierPoint;
    public GameObject WeaponModel;
}
