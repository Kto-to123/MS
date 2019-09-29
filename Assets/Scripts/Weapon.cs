using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Менеджер вооружения, он отвечает за боевую систему.
//Здесь собранна стрельба и смена оружия
public class Weapon : MonoBehaviour
{
    public static Weapon instance;

    // Ячейки для лука и стрел
    public int mainAmmo;
    GameObject activArrow;
    public Transform bowPoint;

    // Основное оружие
    bool MainWeaponActiv;
    static GameObject MainWeaponPrefab;
    static WeaponScript mainWeapon;
    static int mainWeaponID;

    // Метательное оружие
    public bool throwingWeaponActive = false;
    public int myAmmunition = 0;
    public int myWeaponID = 0;
    ElementThrowingWeapons ThrowingWeapon;
    //Метательное оружие не имеет своего класа, как основное т.к поведение разных типов не сильно отличается.

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

    void Update()
    {
        // Проверка нажатий клавиш стрельбы
        if (Input.GetButtonDown("Fire1") && throwingWeaponActive)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)/* && bow1.activeSelf*/)
        {
            MainAttack();
        }
    }

    public void MainWeaponSetActiv(bool v) // Скрытие основного оружия
    {
        MainWeaponPrefab.SetActive(v);
        MainWeaponActiv = v;
    }

    void MainAttack() // Атака основного оружия
    {
        if (mainWeapon != null)
        {
            mainWeapon.Attack();
        }
    }

    void Shoot() // Бросок метательного оружия
    {
        if (myAmmunition > 0)
        {
            Instantiate(ThrowingWeapon.bullet, ThrowingWeapon.fierPoint.position, ThrowingWeapon.fierPoint.rotation);
            myAmmunition--;
            UIManager.instance.SetAmmo(myAmmunition);
        }
    }
    
    public static void DropMainWeapon() // Выбросить основное оружие
    {
        if (mainWeapon != null)
        {
            Instantiate(WeaponDataManagerScript.instance.GetMainDropPrefab(mainWeapon.ID));
            mainWeapon = null;
            Destroy(MainWeaponPrefab);
        }
    }

    public static void InstantMainWeapon(int _id) // Включение основного оружия
    {
        mainWeaponID = _id;
        DropMainWeapon();
        MainWeaponPrefab = WeaponDataManagerScript.instance.GetMainWeapon(_id);
        MainWeaponPrefab = Instantiate(MainWeaponPrefab, instance.bowPoint);
        mainWeapon = MainWeaponPrefab.GetComponent<WeaponScript>();
    }

    public void DropWeapon() // Выбросить метательное оружие
    {
        if (throwingWeaponActive)
        {
            GameObject drop;
            drop = Instantiate(ThrowingWeapon.WeaponModel, ThrowingWeapon.fierPoint.position, ThrowingWeapon.fierPoint.rotation);
            drop.GetComponent<DropWeapom>().count = myAmmunition;
            myAmmunition = 0;
        }
    }

    public void InstantWeapon(int _ID, int _ammunition) // Включение метательного оружия
    {
        DropWeapon();
        myWeaponID = _ID;
        ThrowingWeapon = WeaponDataManagerScript.instance.GetElementThrowingWeapons(_ID);
        throwingWeaponActive = true;
        myAmmunition = _ammunition;
        UIManager.instance.SetWeapon(myWeaponID, myAmmunition);
    }
}
