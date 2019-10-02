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

    [SerializeField]
    public List<ElementInventory> InventoryElements = new List<ElementInventory>();

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

    public ElementInventory GetElementInventory(int _ID)
    {
        return InventoryElements[_ID];
    }

    public int GetInventoryElementCount()
    {
        return InventoryElements.Count;
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

// Список основного оружия
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

// Список метательного оружия
[System.Serializable]
public struct ElementThrowingWeapons
{
    public int ID;
    public GameObject bullet;
    public Transform fierPoint;
    public GameObject WeaponModel;
}

// Список элементов инвентаря
[System.Serializable]
public class ElementInventory
{
    public int id; // Номер объекта
    public string name; // Имя объекта
    public Sprite img; // Изображение объекта
}