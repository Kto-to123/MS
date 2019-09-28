using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BowScript : WeaponScript
{
    //public new GameObject model;

    public GameObject ActivArrow { get; set; }
    public Transform FierPoint;

    public BowScript(int _ID)
    {
        ID = _ID;
    }

    public override void InstantiateThis()
    {
        model = WeaponDataManagerScript.Inst(WeaponDataManagerScript.instance.bow1, WeaponDataManagerScript.instance.BowPoint);
        model.SetActive(true);
    }

    public override void Attack()
    {
        Short();
    }

    void Short()
    {
        WeaponDataManagerScript.Inst(WeaponDataManagerScript.instance.arrow1, WeaponDataManagerScript.instance.BowPoint.position, WeaponDataManagerScript.instance.BowPoint.rotation);
    }
}
