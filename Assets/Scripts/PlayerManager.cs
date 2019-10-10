using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас отвечает за управление компонентами игрока
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int armor = 0;

    void Start()
    {
        
    }

    public void SetArmor(int _Armor)
    {
        armor = _Armor;
        UIManager.instance.SetDefens(armor);
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
