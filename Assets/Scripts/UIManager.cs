using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image ThrowingWeaponIcon;
    public Text amoText;
    public Text armorText;

    public GameObject inventoryUI;
    public GameObject skillPrograsUI;

    private bool weaponActive = true;
    private int myAmmunition = 0;

    #region Structur
    public bool backGroundActive {
        get {
            return inventoryUI.activeSelf;
        }

        set {
            inventoryUI.SetActive(value); 
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

    public void ActivateInterfaceSkillProgras(bool active)
    {
        skillPrograsUI.SetActive(active);
        if (skillPrograsUI.activeSelf)
        {
            Inventory.instance.UpdateInventory();
        }

        Weapon.instance.MainWeaponSetActiv(!active);
    }

    public void ActivateInterfaceInventory(bool active)
    {
        inventoryUI.SetActive(active);
        if (inventoryUI.activeSelf)
        {
            Inventory.instance.UpdateInventory();
        }

        Weapon.instance.MainWeaponSetActiv(!active);
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
