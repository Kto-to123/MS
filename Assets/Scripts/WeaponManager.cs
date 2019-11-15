using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Основное оружие
    bool mainWeaponInst = false;
    GameObject MainWeaponPrefab;
    WeaponScript mainWeapon;
    ElementMainWeapns mainElements;

    // Метательное оружие
    bool throwingWeaponActive = false;
    int throwingAmmunition = 0;
    ElementThrowingWeapons throwingElement;
    // Метательное оружие не имеет своего класа как основное т.к поведение разных типов не отличается.

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
    /// Альтернативная атака основного оружия
    /// </summary>
    public void MainAlternativeAttack()
    {
        if (mainWeapon != null && mainElements.activ)
        {
            mainWeapon.AlternativeAttack();
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
    public void InstantMainWeapon(int _id, Transform instantPoint, int layer)
    {
        DropMainWeapon();
        mainElements = WeaponDataManagerScript.instance.GetElementMainWeapns(_id);
        MainWeaponPrefab = WeaponDataManagerScript.instance.GetMainWeapon(_id);
        MainWeaponPrefab = Instantiate(MainWeaponPrefab, instantPoint);
        mainWeapon = MainWeaponPrefab.GetComponent<WeaponScript>();
        mainWeapon.gameObject.layer = layer;
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
}
