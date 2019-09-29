using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас отвечает за управление компонентами игрока
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

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

    void Start()
    {
        Weapon.InstantMainWeapon(1);
    }
}
