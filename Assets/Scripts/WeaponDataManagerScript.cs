using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//Клас отвечает за хранение информации о снаряжении и выдает данные по ID
public class WeaponDataManagerScript : MonoBehaviour
{
    public static WeaponDataManagerScript instance;

    // Список основного оружия
    [SerializeField]
    List<ElementMainWeapns> MainElements = new List<ElementMainWeapns>();

    //Список метательного оружия
    [SerializeField]
    List<ElementThrowingWeapons> ThrowingElements = new List<ElementThrowingWeapons>();

    //Список элементов инвентаря
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
    public int ID;
    public int inventoryID;
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
    public int inventoryID;
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
    public int mainWeapon; // Использование элемента как основное оружие
    public int throwingWeapon; // Использование элемента как метательное оружие
    public bool stacked; // Можно ли складывать больше одной ячейки в инвентарь
}