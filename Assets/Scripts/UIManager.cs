using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Клас отвечающий за пользовательский интерфейс

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Активный режим интерфейса
    public UImode activUImode;

    [SerializeField] Image ThrowingWeaponIcon; // Значек отображающий активное метательное оружие
    [SerializeField] Text amoText; // Боезапас метательного оружия
    [SerializeField] Text armorText; // Отрожает количество брони

    // Видиы интерфейса
    [SerializeField] GameObject inventoryUI; // Интерфейс отображаемый при открытии инвентаря
    [SerializeField] GameObject skillPrograsUI; // Интерфейс отображаемый при открытии меню прокачки
    [SerializeField] GameObject GameUI; // Игровой интерфейс

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

    // Установка режима UI
    public void SetUIMode(UImode mode)
    {
        switch (mode)
        {
            case UImode.Game:
                UIClose();
                break;
            case UImode.Inventory:
                ActivateInterfaceInventory();
                break;
            case UImode.Skills:
                ActivateInterfaceSkillProgras();
                break;
        }
    }

    // Закрытие не игрового интерфейса
    void UIClose()
    {
        Cursor.visible = false;
        inventoryUI.SetActive(false);
        skillPrograsUI.SetActive(false);
        GameUI.SetActive(true);
        activUImode = UImode.Game;
    }

    // Активация интерфейса инвентаря
    void ActivateInterfaceInventory()
    {
        if (!inventoryUI.activeSelf)
        {
            UIClose();
            Cursor.visible = true;
            inventoryUI.SetActive(true);
            Inventory.instance.UpdateInventory();
            activUImode = UImode.Inventory;
        }
        else
        {
            UIClose();
        }
    }

    // Активация интерфейса скилов
    void ActivateInterfaceSkillProgras()
    {
        if (!skillPrograsUI.activeSelf)
        {
            UIClose();
            Cursor.visible = true;
            skillPrograsUI.SetActive(true);
            activUImode = UImode.Skills;
        }
        else
        {
            UIClose();
        }
    }    

    // Получение количества метательного оружия
    public void SetAmmo(int ammo)
    {
        amoText.text = ammo.ToString();
    }

    // Получение метательного оружия
    public void SetWeapon(int WeaponID, int ammo)
    {
        ThrowingWeaponIcon.sprite = WeaponDataManagerScript.instance.GetThrowingImg(WeaponID);
        amoText.text = ammo.ToString();
    }

    // Получение количества брони
    public void SetDefens(int defens)
    {
        armorText.text = "Защита: " + defens.ToString();
    }
}

public enum UImode
{
    Game,
    Inventory,
    Skills
}