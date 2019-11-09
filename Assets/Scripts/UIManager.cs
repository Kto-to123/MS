using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Клас отвечающий за пользовательский интерфейс

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Активный режим интерфейса
    public UImode activUImode;

    [SerializeField] Image ThrowingWeaponIcon; // Значек отображающий активное метательное оружие
    [SerializeField] Text amoText; // Боезапас метательного оружия
    [SerializeField] Text armorText; // Отрожает количество брони
    [SerializeField] Text healthText; // HP
    [SerializeField] Slider healthSlider;

    // Видиы интерфейса
    [SerializeField] GameObject inventoryUI; // Интерфейс отображаемый при открытии инвентаря
    [SerializeField] GameObject skillPrograsUI; // Интерфейс отображаемый при открытии меню прокачки
    [SerializeField] GameObject GameUI; // Игровой интерфейс
    [SerializeField] GameObject DieUI;

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

    private void Start()
    {
        DieUI.SetActive(false);
        activUImode = UImode.Game;
        SetUIMode(UImode.Game);
        healthSlider.value = 100;
    }

    /// <summary>
    /// Установка режима UI
    /// </summary>
    /// <param name="mode"></param>
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

    /// <summary>
    /// Закрытие не игрового интерфейса
    /// </summary>
    void UIClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventoryUI.SetActive(false);
        skillPrograsUI.SetActive(false);
        GameUI.SetActive(true);
        activUImode = UImode.Game;
    }

    /// <summary>
    /// Активация интерфейса инвентаря
    /// </summary>
    void ActivateInterfaceInventory()
    {
        if (!inventoryUI.activeSelf)
        {
            UIClose();
            Cursor.lockState = CursorLockMode.None;
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

    /// <summary>
    /// Активация интерфейса скилов
    /// </summary>
    void ActivateInterfaceSkillProgras()
    {
        if (!skillPrograsUI.activeSelf)
        {
            UIClose();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            skillPrograsUI.SetActive(true);
            activUImode = UImode.Skills;
        }
        else
        {
            UIClose();
        }
    }

    /// <summary>
    /// Установка количества метательного оружия
    /// </summary>
    /// <param name="ammo"></param>
    public void SetAmmo(int ammo)
    {
        amoText.text = ammo.ToString();
    }

    /// <summary>
    /// Установка метательного оружия
    /// </summary>
    /// <param name="WeaponID"></param>
    /// <param name="ammo"></param>
    public void SetWeapon(int WeaponID, int ammo)
    {
        ThrowingWeaponIcon.sprite = WeaponDataManagerScript.instance.GetThrowingImg(WeaponID);
        amoText.text = ammo.ToString();
    }

    /// <summary>
    /// Установка количества брони
    /// </summary>
    /// <param name="defens"></param>
    public void SetDefens(int defens)
    {
        armorText.text = "Защита: " + defens.ToString();
    }

    /// <summary>
    /// Установка количества здоровья
    /// </summary>
    /// <param name="_health"></param>
    public void SetHealth(int _health)
    {
        healthText.text = _health.ToString();
        healthSlider.value = _health;
    }

    /// <summary>
    /// Смерть
    /// </summary>
    public void Death()
    {
        Cursor.visible = false;
        inventoryUI.SetActive(false);
        skillPrograsUI.SetActive(false);
        GameUI.SetActive(false);
        DieUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Перезапуск уровня
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public enum UImode
{
    Game,
    Inventory,
    Skills
}