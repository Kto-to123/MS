using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас отвечает за управление компонентами игрока
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] double health = 100;
    [SerializeField] int armor = 0;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Установить значение брони
    /// </summary>
    /// <param name="_Armor"></param>
    public void SetArmor(int _Armor)
    {
        armor = _Armor;
        UIManager.instance.SetDefens(armor);
    }

    /// <summary>
    /// Получить урон, бронепробитие = 0
    /// </summary>
    /// <param name="_Damage"></param>
    public void GetDamage(double _Damage)
    {
        health -= armor <= 10 ? _Damage : _Damage / (armor / 10);
        UIManager.instance.SetHealth(((int)health));

        if (health <= 0)
            Death();
    }

    /// <summary>
    /// Получить урон с бронипробитием
    /// </summary>
    /// <param name="_Damage"></param>
    /// <param name="armorPenetration"></param>
    public void GetDamage(double _Damage, int armorPenetration)
    {
        int _brokenArmor = armor - armorPenetration;
        health -= _Damage / _brokenArmor;
        UIManager.instance.SetHealth((int)health);

        if (health <= 0)
            Death();
    }

    /// <summary>
    /// Смерть
    /// </summary>
    void Death()
    {
        gameObject.tag = "Untagged";
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 0, 0);
        GetComponent<PlayerControllerScript>().enabled = false;
        GetComponent<CameraControllerScript>().enabled = false;
        GetComponent<PlayerUsing>().enabled = false;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1.7f, transform.position.z);
        UIManager.instance.Death();
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
