using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image ThrowingWeaponIcon;
    //public GameObject m2;
    //public GameObject m3;
    public Text amoText;
    public Text armorText;

    public GameObject backGround;


    private bool weaponActive = true;
    private int myAmmunition = 0;

    #region Structur
    public bool backGroundActive {
        get {
            return backGround.activeSelf;
        }

        set {
            backGround.SetActive(value); 
        }
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
    #endregion

    void Update()
    {
        
    }

    public void ActivateInterfaceInventory()
    {
        backGround.SetActive(!backGround.activeSelf);
        if (backGround.activeSelf)
        {
            Inventory.instance.UpdateInventory();
        }

        Weapon.instance.MainWeaponSetActiv(!backGround.activeSelf);
    }

    public void SetAmmo(int ammo)
    {
        amoText.text = ammo.ToString();
    }

    public void SetWeapon(int WeaponID, int ammo)
    {
        ThrowingWeaponIcon.sprite = WeaponDataManagerScript.instance.GetThrowingImg(WeaponID);
        amoText.text = ammo.ToString();
    }

    public void SetDefens(int defens)
    {
        armorText.text = "Защита: " + defens.ToString();
    }
}
