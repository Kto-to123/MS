using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Клас отвечает за хранение информации о снаряжении и выдает данные по ID
/// </summary>
public class WeaponDataManagerScript : MonoBehaviour
{
    public static WeaponDataManagerScript instance;

    /// <summary>
    /// Список основного оружия
    /// </summary>
    [SerializeField]
    List<ElementMainWeapns> MainElements = new List<ElementMainWeapns>();

    /// <summary>
    /// Список метательного оружия
    /// </summary>
    [SerializeField]
    List<ElementThrowingWeapons> ThrowingElements = new List<ElementThrowingWeapons>();

    /// <summary>
    /// Список элементов инвентаря
    /// </summary>
    [SerializeField]
    public List<ElementInventory> InventoryElements = new List<ElementInventory>();

    private void Start()
    {
        int i = 0;
        foreach (ElementInventory element in InventoryElements)
        {
            element.id = i;
            if (element.equipment != null)
            {
                element.img = element.equipment.image;
                element.name = element.ToString();
                element.mainWeapon = 0;
            }

            i++;
        }
    }

    /// <summary>
    /// Получить модель основного оружия
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public GameObject GetMainWeapon(int _ID)
    {
        return MainElements[_ID].mainPrefab;
    }

    /// <summary>
    /// Получить подбираемую модель основного оружия
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public GameObject GetMainDropPrefab(int _ID)
    {
        return MainElements[_ID].DropPrefab;
    }

    /// <summary>
    /// Получить основное оружие
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public ElementMainWeapns GetElementMainWeapns(int _ID)
    {
        return MainElements[_ID];
    }

    /// <summary>
    /// Получить модель снаряда метательного оружия
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public GameObject GetThrowingWeapon(int _ID)
    {
        return ThrowingElements[_ID].bullet;
    }

    /// <summary>
    /// Получить подбираемую модель метательного оружия
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public GameObject GetThrowingDropPrefab(int _ID)
    {
        return ThrowingElements[_ID].WeaponModel;
    }

    /// <summary>
    /// Получить метательное оружие
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public ElementThrowingWeapons GetElementThrowingWeapons(int _ID)
    {
        return ThrowingElements[_ID];
    }

    /// <summary>
    /// Получить изображение элемента
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public Sprite GetThrowingImg(int _ID)
    {
        return InventoryElements[ThrowingElements[_ID].inventoryID].img;
    }

    /// <summary>
    /// Получить определенный элемент инвентаря
    /// </summary>
    /// <param name="_ID"></param>
    /// <returns></returns>
    public ElementInventory GetElementInventory(int _ID)
    {
        return InventoryElements[_ID];
    }

    /// <summary>
    /// Получить количество элементов инвентаря
    /// </summary>
    /// <returns></returns>
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

/// <summary>
/// Список основного оружия
/// </summary>
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

/// <summary>
/// Список метательного оружия
/// </summary>
[System.Serializable]
public struct ElementThrowingWeapons
{
    public int ID;
    public int inventoryID;
    public GameObject bullet;
    public Transform fierPoint;
    public GameObject WeaponModel;
}

/// <summary>
/// Список элементов инвентаря
/// </summary>
[System.Serializable]
public class ElementInventory
{
    /// <summary>
    /// Номер объекта
    /// </summary>
    public int id;
    /// <summary>
    /// Имя объекта
    /// </summary>
    public string name;
    /// <summary>
    /// Изображение объекта
    /// </summary>
    public Sprite img;
    /// <summary>
    /// Использование элемента как основное оружие
    /// </summary>
    public int mainWeapon;
    /// <summary>
    /// Использование элемента как метательное оружие
    /// </summary>
    public int throwingWeapon;
    /// <summary>
    /// Использование в качестве боеприпаса
    /// </summary>
    public AmmoType ammoType;
    //public bool stacked; // Можно ли складывать больше одной ячейки в инвентарь
    //public bool equipmentUsebl;
    public Equipment equipment;
}

public enum AmmoType
{
    now,
    arrow,
    bullet
}