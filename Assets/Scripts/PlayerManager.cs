using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас отвечает за управление компонентами игрока
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] int health = 100;
    [SerializeField] int armor = 0;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetArmor(int _Armor)
    {
        armor = _Armor;
        UIManager.instance.SetDefens(armor);
    }

    public void GetDamage(int _Damage)
    {
        health -= armor == 0 ? _Damage : _Damage / armor;
        UIManager.instance.SetHealth(health);

        if (health <= 0)
            Death();
    }

    public void GetDamage(int _Damage, int armorPenetration)
    {
        int _brokenArmor = armor - armorPenetration;
        health -= _Damage / _brokenArmor;
        UIManager.instance.SetHealth(health);

        if (health <= 0)
            Death();
    }

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
