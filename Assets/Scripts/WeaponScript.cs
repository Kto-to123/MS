using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript// : MonoBehaviour
{
    public GameObject model;
    public int ID { get; set; }

    public virtual void Reload()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void InstantiateThis()
    {
        //GameObject.Instantiate()
    }

    public void WeaponSetActiv(bool v) // Скрытие оружия
    {
        model.SetActive(v);
    }
}
